using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.API.Validators;
using wleepay.Communication.Interface;
using wleepay.Communication.Templates;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IWalletService _walletService;
        private readonly ISMSService _smsService;
        private readonly IMapper _mapper;
        public TransactionController(ITransactionService transactionService, IWalletService walletService, ISMSService smsService, IMapper mapper)
        {
            _transactionService = transactionService;
            _walletService = walletService;
            _smsService = smsService;
            _mapper = mapper;
        }

        [HttpPost("History")]
        public async Task<IActionResult> GetTrasactionHistoryByWalletId([FromBody] TransactionPageRequest request)
        {
            var transactionList = await _transactionService.GetTransactionsByWalletId(request.WalletId, request.Index, request.Size);
            if (transactionList.Count() > 0)
            {
                var transactionResourceList = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionHistoryResource>>(transactionList);
                return Ok(transactionResourceList);
            }

            return Ok(new List<TransactionResource>());
        }

        [HttpPost("Report")]
        public async Task<IActionResult> GetTrasactionHistoryByWalletId([FromBody] TransactionReportRequest request)
        {
            var transactionList = await _transactionService.GetTransactionsByType(request.TransferType, request.Index, request.Size);
            if (transactionList.Count() > 0)
            {
                var transactionResourceList = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionHistoryResource>>(transactionList);
                return Ok(transactionResourceList);
            }

            return Ok(new List<TransactionResource>());
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> GetTrasactionById(long transactionId)
        {
            var transaction = await _transactionService.GetTransactionsById(transactionId);
            var transactionResource = _mapper.Map<Transaction, TransactionHistoryResource>(transaction);
            return Ok(transactionResource);
        }

        [HttpGet("{userId}/{currency}/{isMonthly}")]
        public async Task<IActionResult> GetTrasactionById(string userId, int currency, bool isMonthly)
        {
            var wallets = await _walletService.GetWalletsByUserId(userId, currency);
            var wallet = wallets.Where(w => w.Type == WalletTypes.MemberAccount.GetHashCode()).FirstOrDefault();
            if (wallet == null)
                return BadRequest("No Wallet found for user");

            var transactionAmount = await _transactionService.CalculateTotalTransactionAmount(wallet.Id, isMonthly);
            return Ok(transactionAmount);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionResource transactionRequest)
        {
            var validator = new CreateTransactionValidator();
            var validationResult = await validator.ValidateAsync(transactionRequest);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var transactionToCreate = _mapper.Map<CreateTransactionResource, Transaction>(transactionRequest);
            var newTransaction = await _transactionService.InitiateTransaction(transactionToCreate);

            var newTransactionResource = _mapper.Map<Transaction, TransactionResource>(newTransaction);
            if (newTransactionResource != null)
            {
                await SendTransactionActionSMS(newTransaction);
            }
            return Ok(newTransactionResource);
        }

        //[HttpPost]
        //public async Task<IActionResult> IntiateCashout([FromBody] Transaction transactionDetail)
        //{

        //    var newTransaction = await _transactionService.InitiateTransaction(transactionDetail);

        //    var newTransactionResource = _mapper.Map<Transaction, TransactionResource>(newTransaction);
        //    return Ok(newTransactionResource);
        //}

        [HttpPost("request")]
        public async Task<IActionResult> CreatePaymentRequest([FromBody] CreateTransactionResource transactionRequest)
        {
            var validator = new CreateTransactionValidator();
            var validationResult = await validator.ValidateAsync(transactionRequest);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var transactionToCreate = _mapper.Map<CreateTransactionResource, Transaction>(transactionRequest);
            var newTransaction = await _transactionService.InitiateTransactionRequest(transactionToCreate);

            var newTransactionResource = _mapper.Map<Transaction, TransactionResource>(newTransaction);
            if (newTransactionResource != null)
            {
                await SendTransactionActionSMS(newTransaction);
            }

            return Ok(newTransactionResource);
        }

        [HttpPut("approve")]
        public async Task<IActionResult> ApprovePaymentRequest(long paymentRequestId)
        {
            if (paymentRequestId <= 0)
                return BadRequest("Please enter valid payment request Id");

            var transactionToApprove = await _transactionService.GetTransactionsById(paymentRequestId);
            if (transactionToApprove == null)
                return BadRequest("Invalid payment Id");

            var approvedTransaction = new Transaction();
            if (transactionToApprove.TransferTypeId == TransferTypes.CashOut.GetHashCode())
                approvedTransaction = await _transactionService.InitiateCashout(transactionToApprove);
            else
                approvedTransaction = await _transactionService.InitiateTransaction(transactionToApprove);

            var newTransactionResource = _mapper.Map<Transaction, TransactionResource>(approvedTransaction);
            //send sms to user
            if (newTransactionResource != null)
                await SendTransactionActionSMS(approvedTransaction);

            return Ok(newTransactionResource);
        }

        [HttpPut("reject")]
        public async Task<IActionResult> RejectPaymentRequest(long paymentRequestId)
        {
            if (paymentRequestId <= 0)
                return BadRequest("Please enter valid payment request Id");

            var transactionToReject = await _transactionService.GetTransactionsById(paymentRequestId);
            if (transactionToReject == null)
                return BadRequest("Invalid payment Id");

            var result = await ChangePaymentRequestStatus(paymentRequestId, Core.Enums.TransactionStatus.Rejected);
            if (result == null)
                return BadRequest("Invalid payment Id");

            //send sms to user
            if (result != null)
                await SendTransactionActionSMS(transactionToReject);

            return Ok(result);
        }

        [HttpPut("cancel")]
        public async Task<IActionResult> CancelPaymentRequest(long paymentRequestId)
        {
            if (paymentRequestId <= 0)
                return BadRequest("Please enter valid payment request Id");

            var result = await ChangePaymentRequestStatus(paymentRequestId, Core.Enums.TransactionStatus.Cancel);
            if (result == null)
                return BadRequest("Invalid payment Id");

            return Ok(result);
        }

        [HttpGet("Cashout")]
        public async Task<IActionResult> CashoutTransactions()
        {

            var result = await _transactionService.GetCashoutTransactions();
            var transactionResources = result.Select(transaction => _mapper.Map<Transaction, TransactionHistoryResource>(transaction)).ToList();

            if (result == null)
                return BadRequest("Invalid UserId");

            return Ok(transactionResources);
        }

        [HttpGet("TopUp")]
        public async Task<IActionResult> TopUpTransactions()
        {

            var result = await _transactionService.GetTopUpTransactions();
            var transactionResources = result.Select(transaction => _mapper.Map<Transaction, TransactionHistoryResource>(transaction)).ToList();

            if (result == null)
                return BadRequest("Invalid UserId");

            return Ok(transactionResources);
        }


        #region Helper function
        private async Task<TransactionResource> ChangePaymentRequestStatus(long paymentRequestId, Core.Enums.TransactionStatus status)
        {

            var transactionToApprove = await _transactionService.GetTransactionsById(paymentRequestId);
            if (transactionToApprove == null)
                return null;

            var rejectedTransaction = await _transactionService.UpdateTransactionStatus(paymentRequestId, status);
            return _mapper.Map<Transaction, TransactionResource>(rejectedTransaction);
        }

        private async Task<string> SendTransactionActionSMS(Transaction transaction)
        {
            var userWallet = new Wallet();
            string messageText = string.Empty;
            var status = (Core.Enums.TransactionStatus)transaction.Status;
            //Get from wallet user details
            var fromWalletUser = await _walletService.GetUserDetailsByWalletIdAndCurrency(transaction.FromWalletId);
            //Get to wallet user details
            var toWalletUser = await _walletService.GetUserDetailsByWalletIdAndCurrency(transaction.ToWalletId);

            //check transaction type
            if (transaction.TransferTypeId == Core.Enums.TransferTypes.TopUp.GetHashCode() ||
                transaction.TransferTypeId == Core.Enums.TransferTypes.FillAgent.GetHashCode())
            {
                userWallet = (status == Core.Enums.TransactionStatus.Pending) ? fromWalletUser : toWalletUser;
                string name = (status == Core.Enums.TransactionStatus.Pending)
                    ? $"{toWalletUser.ApplicationUser.UserDetail.FirstName} {toWalletUser.ApplicationUser.UserDetail.LastName}"
                    : $"{fromWalletUser.ApplicationUser.UserDetail.FirstName} {fromWalletUser.ApplicationUser.UserDetail.LastName}";
                messageText = GenerateMessageText(transaction.Amount, fromWalletUser.Currency.Name, name, transaction.Id, status, transaction.TransferTypeId);
            }
            else if (transaction.TransferTypeId == Core.Enums.TransferTypes.CashOut.GetHashCode() ||
               transaction.TransferTypeId == Core.Enums.TransferTypes.AgentCommissionCashout.GetHashCode())
            {
                userWallet = (status == Core.Enums.TransactionStatus.Pending) ? toWalletUser : fromWalletUser;
                string name = (status == Core.Enums.TransactionStatus.Pending)
                    ? $"{fromWalletUser.ApplicationUser.UserDetail.FirstName} {fromWalletUser.ApplicationUser.UserDetail.LastName}"
                    : $"{toWalletUser.ApplicationUser.UserDetail.FirstName} {toWalletUser.ApplicationUser.UserDetail.LastName}";
                messageText = GenerateMessageText(transaction.Amount, userWallet.Currency.Name, name, transaction.Id, status, transaction.TransferTypeId, true);
            }
            else if (transaction.TransferTypeId == Core.Enums.TransferTypes.WleeTransfer.GetHashCode())
            {
                userWallet = toWalletUser;
                string name = $"{fromWalletUser.ApplicationUser.UserDetail.FirstName} {fromWalletUser.ApplicationUser.UserDetail.LastName}";
                messageText = GenerateMessageText(transaction.Amount, userWallet.Currency.Name, name, transaction.Id, status, transaction.TransferTypeId, true);
            }

            string result = string.Empty;

            if (!string.IsNullOrEmpty(messageText))
                result = _smsService.sendMessage(messageText, userWallet.ApplicationUser.UserDetail.PhoneNumber);

            return result;
        }

        private string GenerateMessageText(double amount, string currency, string userName, long transactionId, Core.Enums.TransactionStatus status, int transactionTypeId, bool isCashout = false)
        {
            string messageText = string.Empty;
            string amountString = amount.ToString("C2");
            switch (status)
            {
                case Core.Enums.TransactionStatus.Pending:
                    messageText = MessagesTemplates.PaymentRequestReceived(amountString, currency, userName, transactionId, isCashout);
                    break;

                case Core.Enums.TransactionStatus.Approved:
                    if (transactionTypeId == Core.Enums.TransferTypes.WleeTransfer.GetHashCode())
                        messageText = MessagesTemplates.PaymentReceived(amountString, currency, userName, transactionId);
                    else
                        messageText = MessagesTemplates.PaymentApprovedRejected(amountString, currency, userName, transactionId, isCashout);
                    break;

                case Core.Enums.TransactionStatus.Rejected:
                    messageText = MessagesTemplates.PaymentApprovedRejected(amountString, currency, userName, transactionId, isCashout, true);
                    break;
            }
            return messageText;
        }
        #endregion
    }
}
