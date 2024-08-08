using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Middleware;
using wleepay.API.Resources;
using wleepay.API.Validators;
using wleepay.Communication.Interface;
using wleepay.Communication.Templates;
using wleepay.Core.DTO.Request;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Services;
using Currency = wleepay.Core.Models.Currency;
using Status = wleepay.Core.Enums.Status;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    //[AllowAnonymous]

    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IWalletService _walletService;
        private readonly ISMSService _smsService;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly ICashFlowService _cashFlowService;
        private readonly IUserDetailService _userDetailService;
        private readonly IFirebaseNotification _firebaseNotification;
        private readonly IUserDeviceService _userDeviceService;


        public TransactionController(ITransactionService transactionService, IWalletService walletService, ISMSService smsService, IMapper mapper, IEmailSender emailSender, ICashFlowService cashFlowService, IUserDetailService userDetailService, IFirebaseNotification firebaseNotification, IUserDeviceService userDeviceService) 
        {
            _transactionService = transactionService;
            _walletService = walletService;
            _smsService = smsService;
            _mapper = mapper;
            _emailSender = emailSender;
            _cashFlowService = cashFlowService;
            _userDetailService = userDetailService;
            _firebaseNotification = firebaseNotification;
            _userDeviceService = userDeviceService;
        }

        [HttpPost("History")]
        public async Task<IActionResult> GetTrasactionHistoryByWalletId([FromBody] TransactionPageRequest request)
        {
            var transactionResult = await _transactionService.GetTransactionsByWalletId(request.WalletId, request.Index, request.Size);
            if (transactionResult.Transactions.Count() > 0)
            {
                var transactionResourceList = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionHistoryResource>>(transactionResult.Transactions);

                foreach (var item in transactionResourceList)
                {
                    var time = Helpers.Common.ConvertISTtoLiberiaTime(item.CreatedDate);
                    item.CreatedDate = time;
                }
                return Ok(new { Transactions = transactionResourceList, TotalCount = transactionResult.TotalCount });
            }

            return Ok(new List<TransactionResource>());
        }

        [HttpPost("Report")]
        public async Task<IActionResult> GetTransactionsByType([FromBody] TransactionReportRequest request)
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
        public async Task<IActionResult> GetTrasactionAmount(string userId, int currency, bool isMonthly)
        {
            var wallets = await _walletService.GetWalletsByUserId(userId, currency);
            var wallet = wallets.Where(w => w.Type == WalletTypes.MemberAccount.GetHashCode()).FirstOrDefault();
            if (wallet == null)
                return BadRequest("No Wallet found for user");

            var transactionAmount = await _transactionService.CalculateTotalTransactionAmount(wallet.Id, isMonthly);
            return Ok(transactionAmount);
        }

        [SystemLock]
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionResource transactionRequest)
        {
            var validator = new CreateTransactionValidator();
            var validationResult = await validator.ValidateAsync(transactionRequest);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var walletBalance = await _walletService.GetWallet(transactionRequest.FromWalletId);
            if (walletBalance==null)
            {
                return NotFound();
            }

            var aplicationUserDetail = await _userDetailService.GetApplicationUserById(walletBalance.UserId);
            if ((aplicationUserDetail.UserRoles.FirstOrDefault().Role.Name == "Agent" && walletBalance.ApplicationUser.UserDetail.Status == (int)Status.Suspend) && transactionRequest.TransferTypeId != (int)TransferTypes.AgentCommissionCashout)
            {
                return BadRequest("Agent cannot make transaction when account is suspended. He/she can only cashout agent comminsion and wallet balances.");
            }
            if (transactionRequest != null && transactionRequest.TransferTypeId == (int)TransferTypes.TopUpSystemWallet)
            {
                CashFlowHistory cashFlow = new CashFlowHistory();
                cashFlow.TotalAmount = walletBalance.Balance - transactionRequest.Amount;
                cashFlow.WithdrawAmount = transactionRequest.Amount;
                cashFlow.WalletId = transactionRequest.FromWalletId;
                cashFlow.CurrencyId = walletBalance.CurrencyId;
                cashFlow.PreviousAmount = walletBalance.Balance;

                await _cashFlowService.AddCashFlowHistoryForWithdraw(cashFlow);
            }

            var transactionToCreate = _mapper.Map<CreateTransactionResource, Transaction>(transactionRequest);
            var newTransaction = await _transactionService.InitiateTransaction(transactionToCreate);
            var newTransactionResource = _mapper.Map<Transaction, TransactionResource>(newTransaction);

            if (newTransactionResource != null)
            {

                await SendTransactionActionSMS(newTransaction);

                if (transactionRequest.TransferTypeId == Core.Enums.TransferTypes.TopUpSystemWallet.GetHashCode())
                {
                    await SendTransactionActionEmail(newTransaction);

                }
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

        [SystemLock]
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
                await SendTransactionRequestActionSMS(newTransaction);
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

        [HttpPut("BulkApprove")]
        public async Task<IActionResult> ApprovePaymentRequest(List<long> paymentRequestIds)
        {
            TransactionResource newTransactionResource = new TransactionResource();
            foreach (var paymentRequestId in paymentRequestIds)
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

                   newTransactionResource = _mapper.Map<Transaction, TransactionResource>(approvedTransaction);
                //send sms to user
                if (newTransactionResource != null)
                    await SendTransactionActionSMS(approvedTransaction);
            }

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

        [HttpPut("bulkReject")]
        public async Task<IActionResult> RejectPaymentRequest(List<long> paymentRequestIds)
        {
            TransactionResource result = new TransactionResource();
            foreach (var paymentRequestId in paymentRequestIds)
            {

                if (paymentRequestId <= 0)
                    return BadRequest("Please enter valid payment request Id");

                var transactionToReject = await _transactionService.GetTransactionsById(paymentRequestId);
                if (transactionToReject == null)
                    return BadRequest("Invalid payment Id");

                result = await ChangePaymentRequestStatus(paymentRequestId, Core.Enums.TransactionStatus.Rejected);
                if (result == null)
                    return BadRequest("Invalid payment Id");

                //send sms to user
                if (result != null)
                    await SendTransactionActionSMS(transactionToReject);
            }

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
        public async Task<IActionResult> CashoutTransactions([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {

            var result = await _transactionService.GetCashoutTransactions(pageIndex, pageSize);
            var transactionResources = result.Transactions.Select(transaction => _mapper.Map<Transaction, TransactionHistoryResource>(transaction)).ToList();

            if (result == null)
                return BadRequest("Invalid UserId");

            return Ok(new
            {
                TotalCount = result.TotalCount,
                Transactions = transactionResources
            });
        }

        [HttpGet("TopUp")]
        public async Task<IActionResult> TopUpTransactions([FromQuery]int pageIndex=0 , [FromQuery] int pageSize=10)
        {

            var result = await _transactionService.GetTopUpTransactions(pageIndex, pageSize);
            var transactionResources = result.Transactions.Select(transaction => _mapper.Map<Transaction, TransactionHistoryResource>(transaction)).ToList();

            if (result == null)
                return BadRequest("Invalid UserId");

            return Ok(new
            {
                TotalCount = result.TotalCount,
                Transactions = transactionResources
            });

        }

        [HttpGet("{transferTypeId}/{currencyTypeId}")]
        public async Task<IActionResult> GetTransactionByTypeAndCurrency(int transferTypeId,int currencyTypeId, [FromQuery] int pageIndex , [FromQuery] int pageSize)
        {

            var result = await _transactionService.GetTransactionsByTypeAndCurrency(transferTypeId, currencyTypeId, pageIndex, pageSize);
            var transactionResources = result.Transactions.Select(transaction => _mapper.Map<Transaction, TransactionHistoryResource>(transaction)).ToList();

            if (result == null)
                return BadRequest("Invalid UserId");

            return Ok(new
            {
                TotalCount = result.TotalCount,
                Transactions = transactionResources
            });
        }

        [HttpGet("type/{transferType}/{userId}")]
        public async Task<IActionResult> GetTransactionsByUserIdAndType(string userId, int transferType)
        {
            var transactionResult = await _transactionService.GetTransactionsByUserIdAndType(userId, transferType);
            if (transactionResult.Transactions.Count() > 0)
            {
                var transactionResourceList = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionHistoryResource>>(transactionResult.Transactions);
                return Ok(new { Transactions = transactionResourceList, TotalCount = transactionResult.TotalCount });
            }

            return Ok(new List<TransactionResource>());
        }

        [AllowAnonymous]
        [HttpGet("history/{userId}")]
        public async Task<IActionResult> GetTransactionHistoryByUserId(string userId, [FromQuery] int Index, [FromQuery] int Size)
        {
            var transactionResult = await _transactionService.GetTransactionHistoryByUserId(userId, Index, Size);
            if (transactionResult.Transactions.Count() > 0)
            {
                var transactionResourceList = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionHistoryResource>>(transactionResult.Transactions);
                return Ok(new { Transactions = transactionResourceList, TotalCount = transactionResult.TotalCount });
            }

            return Ok(new List<TransactionResource>());
        }

        [HttpPost("agentPaymentRequest")]
        public async Task<IActionResult> GetIncomingOutGoingTopUpRequests([FromBody] AgentPaymentRequest paymentRequestParam)
        {
            var paymentRequest = await _transactionService.GetPaymentRequestByStatusAndType(paymentRequestParam.Type, paymentRequestParam.TransferStatus, paymentRequestParam.TransferId ,paymentRequestParam.PageSize, paymentRequestParam.PageIndex);

            if (paymentRequest == null)
                return Ok(new List<TransactionResource>());

            var transactionResource = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionHistoryResource>>(paymentRequest);
           
            foreach (var item in transactionResource)
            {
                var time = Helpers.Common.ConvertISTtoLiberiaTime(item.CreatedDate);
                item.CreatedDate = time;
            }
            return Ok(transactionResource);
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
            string result = string.Empty;

            var status = (Core.Enums.TransactionStatus)transaction.Status;
            //Get from wallet user details
            var fromWalletUser = await _walletService.GetUserDetailsByWalletIdAndCurrency(transaction.FromWalletId);
            //Get to wallet user details
            var toWalletUser = await _walletService.GetUserDetailsByWalletIdAndCurrency(transaction.ToWalletId);

            FirebaseMessageRequest firebaseMessageRequest = new FirebaseMessageRequest()
            {
                Title = "Payment recived",
                Body = "",
                DeviceToken = "",
                DeviceId = "",
                UserId = toWalletUser.ApplicationUser.UserDetail.UserId
            };
            
            //check transaction type
            if (transaction.TransferTypeId == Core.Enums.TransferTypes.TopUp.GetHashCode() ||
                transaction.TransferTypeId == Core.Enums.TransferTypes.FillAgent.GetHashCode())
            {
                var device = await _userDeviceService.GetUserDeviceByUserId(toWalletUser.ApplicationUser.UserDetail.UserId);

                foreach (var item in device)
                {
                    firebaseMessageRequest.DeviceId = item.DeviceId;
                    firebaseMessageRequest.DeviceToken = item.FcmToken;
                    firebaseMessageRequest.Body = "Top up request approved  for $" + transaction.Amount;
                    var sendNotification = _firebaseNotification.SendFirebaseMessageRequest(firebaseMessageRequest);
                }
                 userWallet = (status == Core.Enums.TransactionStatus.Pending) ? fromWalletUser : toWalletUser;
                string name = (status == Core.Enums.TransactionStatus.Pending)
                    ? $"{toWalletUser.ApplicationUser.UserDetail.FirstName} {toWalletUser.ApplicationUser.UserDetail.LastName}"
                    : $"{fromWalletUser.ApplicationUser.UserDetail.FirstName} {fromWalletUser.ApplicationUser.UserDetail.LastName}";
                messageText = GenerateMessageText(transaction.Amount, fromWalletUser.Currency, name, transaction.Id, status, transaction.TransferTypeId);
            }
            else if (transaction.TransferTypeId == Core.Enums.TransferTypes.CashOut.GetHashCode() ||
               transaction.TransferTypeId == Core.Enums.TransferTypes.AgentCommissionCashout.GetHashCode())
            {
                var device = await _userDeviceService.GetUserDeviceByUserId(toWalletUser.ApplicationUser.UserDetail.UserId);

                foreach (var item in device)
                {
                    firebaseMessageRequest.DeviceId = item.DeviceId;
                    firebaseMessageRequest.DeviceToken = item.FcmToken;
                    firebaseMessageRequest.Body = "Cash out request approved for $" + transaction.Amount;

                    var sendNotification = _firebaseNotification.SendFirebaseMessageRequest(firebaseMessageRequest);
                }

                userWallet = (status == Core.Enums.TransactionStatus.Pending) ? toWalletUser : fromWalletUser;
                string name = (status == Core.Enums.TransactionStatus.Pending)
                    ? $"{fromWalletUser.ApplicationUser.UserDetail.FirstName} {fromWalletUser.ApplicationUser.UserDetail.LastName}"
                    : $"{toWalletUser.ApplicationUser.UserDetail.FirstName} {toWalletUser.ApplicationUser.UserDetail.LastName}";
                messageText = GenerateMessageText(transaction.Amount, userWallet.Currency, name, transaction.Id, status, transaction.TransferTypeId, true);
            }
            else if (transaction.TransferTypeId == Core.Enums.TransferTypes.WleeTransfer.GetHashCode())
            {
                var device = await _userDeviceService.GetUserDeviceByUserId(toWalletUser.ApplicationUser.UserDetail.UserId);

                foreach (var item in device)
                {
                    firebaseMessageRequest.DeviceId = item.DeviceId;
                    firebaseMessageRequest.DeviceToken = item.FcmToken;
                    firebaseMessageRequest.Body = "Payment recived of amount $" + transaction.Amount;

                    var sendNotification = _firebaseNotification.SendFirebaseMessageRequest(firebaseMessageRequest);
                }

                userWallet = toWalletUser;
                string name = $"{fromWalletUser.ApplicationUser.UserDetail.FirstName} {fromWalletUser.ApplicationUser.UserDetail.LastName}";
                messageText = GenerateMessageText(transaction.Amount, userWallet.Currency, name, transaction.Id, status, transaction.TransferTypeId, true);
            }
            else if (transaction.TransferTypeId == Core.Enums.TransferTypes.TopUpSystemWallet.GetHashCode())
            {
                userWallet = toWalletUser;
                string name = $"{fromWalletUser.ApplicationUser.UserDetail.FirstName} {fromWalletUser.ApplicationUser.UserDetail.LastName}";
                messageText = GenerateMessageText(transaction.Amount, userWallet.Currency, name, transaction.Id, status, transaction.TransferTypeId, true);
                var allAgents = await _userDetailService.GetAllUsersById(Role.Agent, 0, 0, userWallet.ApplicationUser.Id);
                var agentsListResource = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(allAgents.UserDetails);

                foreach (var agent in agentsListResource)
                {
                    result = _smsService.sendMessage(messageText, agent.UserDetail.PhoneNumber);
                }
                return result;
            }

            if (!string.IsNullOrEmpty(messageText))
                result = _smsService.sendMessage(messageText, userWallet.ApplicationUser.UserDetail.PhoneNumber);

            return result;
        }

        private async Task<string> SendTransactionRequestActionSMS(Transaction transaction)
        {
            var userWallet = new Wallet();
            string messageText = string.Empty;
            string result = string.Empty;

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
                messageText = GenerateMessageText(transaction.Amount, fromWalletUser.Currency, name, transaction.Id, status, transaction.TransferTypeId);
            }
            else if (transaction.TransferTypeId == Core.Enums.TransferTypes.CashOut.GetHashCode() ||
               transaction.TransferTypeId == Core.Enums.TransferTypes.AgentCommissionCashout.GetHashCode())
            {
                userWallet = (status == Core.Enums.TransactionStatus.Pending) ? toWalletUser : fromWalletUser;
                string name = (status == Core.Enums.TransactionStatus.Pending)
                    ? $"{fromWalletUser.ApplicationUser.UserDetail.FirstName} {fromWalletUser.ApplicationUser.UserDetail.LastName}"
                    : $"{toWalletUser.ApplicationUser.UserDetail.FirstName} {toWalletUser.ApplicationUser.UserDetail.LastName}";
                messageText = GenerateMessageText(transaction.Amount, userWallet.Currency, name, transaction.Id, status, transaction.TransferTypeId, true);
            }
            else if (transaction.TransferTypeId == Core.Enums.TransferTypes.WleeTransfer.GetHashCode())
            {
                userWallet = toWalletUser;
                string name = $"{fromWalletUser.ApplicationUser.UserDetail.FirstName} {fromWalletUser.ApplicationUser.UserDetail.LastName}";
                messageText = GenerateMessageText(transaction.Amount, userWallet.Currency, name, transaction.Id, status, transaction.TransferTypeId, true);
            }
            else if (transaction.TransferTypeId == Core.Enums.TransferTypes.TopUpSystemWallet.GetHashCode())
            {
                userWallet = toWalletUser;
                string name = $"{fromWalletUser.ApplicationUser.UserDetail.FirstName} {fromWalletUser.ApplicationUser.UserDetail.LastName}";
                messageText = GenerateMessageText(transaction.Amount, userWallet.Currency, name, transaction.Id, status, transaction.TransferTypeId, true);
                var allAgents = await _userDetailService.GetAllUsersById(Role.Agent, 0, 0, userWallet.ApplicationUser.Id);
                var agentsListResource = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(allAgents.UserDetails);

                foreach (var agent in agentsListResource)
                {
                    result = _smsService.sendMessage(messageText, agent.UserDetail.PhoneNumber);
                }
                return result;
            }

            if (!string.IsNullOrEmpty(messageText))
                result = _smsService.sendMessage(messageText, userWallet.ApplicationUser.UserDetail.PhoneNumber);

            return result;
        }

        private string GenerateMessageText(double amount, Currency currency, string userName, long transactionId, Core.Enums.TransactionStatus status, int transactionTypeId, bool isCashout = false)
        {
            string messageText = string.Empty;
            string amountString = amount.ToString("C2", new CultureInfo("en-US"));
            string currencyName = currency.Symbol == "L$" ? "LRD" : "USD";

            switch (status)
            {
                case Core.Enums.TransactionStatus.Pending:
                    messageText = MessagesTemplates.PaymentRequestReceived(amountString, currencyName, userName, transactionId, isCashout);
                    break;

                case Core.Enums.TransactionStatus.Approved:
                    if (transactionTypeId == Core.Enums.TransferTypes.WleeTransfer.GetHashCode())
                        messageText = MessagesTemplates.PaymentReceived(amountString, currencyName, userName, transactionId);
                    else if (transactionTypeId == Core.Enums.TransferTypes.TopUpSystemWallet.GetHashCode())
                        messageText = MessagesTemplates.PaymentReceived(amountString, currencyName, userName, transactionId);
                    else
                        messageText = MessagesTemplates.PaymentApprovedRejected(amountString, currencyName, userName, transactionId, isCashout);
                    break;

                case Core.Enums.TransactionStatus.Rejected:
                    messageText = MessagesTemplates.PaymentApprovedRejected(amountString, currencyName, userName, transactionId, isCashout, true);
                    break;
            }
            return messageText;
        }

        private async Task<string> SendTransactionActionEmail(Transaction transaction)
        {
            var userWallet = new Wallet();
            string emailText = string.Empty;

            var status = (Core.Enums.TransactionStatus)transaction.Status;

            //Get from wallet user details
            var fromWalletUser = await _walletService.GetUserDetailsByWalletIdAndCurrency(transaction.FromWalletId);

            //Get to wallet user details
            var toWalletUser = await _walletService.GetUserDetailsByWalletIdAndCurrency(transaction.ToWalletId);

            //check transaction type
            if (transaction.TransferTypeId == Core.Enums.TransferTypes.TopUpSystemWallet.GetHashCode())
            {
                userWallet = toWalletUser;
                string recieverName = $"{userWallet.ApplicationUser.UserDetail.FirstName} {userWallet.ApplicationUser.UserDetail.LastName}";
                string name = $"{fromWalletUser.ApplicationUser.UserDetail.FirstName} {fromWalletUser.ApplicationUser.UserDetail.LastName}";
                emailText = GenerateEmailText(transaction.Amount, userWallet.Currency, name, transaction.Id, status, transaction.TransferTypeId, true, recieverName);
            }

            string result = string.Empty;

            if (!string.IsNullOrEmpty(emailText))
                result = await _emailSender.EmailForPaymentRecieved(emailText, userWallet.ApplicationUser.Email);

            return result;
        }

        private string GenerateEmailText(double amount, Currency currency, string userName, long transactionId, Core.Enums.TransactionStatus status, int transactionTypeId, bool isCashout = false, string recieverName = "")
        {
            string messageText = string.Empty;
            string amountString = amount.ToString("C2", new CultureInfo("en-US"));
            string currencyName = currency.Symbol == "L$" ? "LRD" : "USD";

            if (transactionTypeId == Core.Enums.TransferTypes.TopUpSystemWallet.GetHashCode())
                messageText = MessagesTemplates.PaymentReceivedEmail(amountString, currencyName, userName, transactionId, recieverName);

            return messageText;
        }

        #endregion
    }
}
