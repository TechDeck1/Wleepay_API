using AutoMapper;
using FirebaseAdmin.Messaging;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Helpers;
using wleepay.API.Middleware;
using wleepay.API.Resources;
using wleepay.API.Validators;
using wleepay.Communication.Interface;
using wleepay.Communication.Templates;
using wleepay.Core.Common;
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
        private readonly ISystemStatusService _statusService;
        private readonly IFirebaseNotification _firebaseNotification;
        private readonly IUserDeviceService _userDeviceService;
        private readonly ISystemActivityLogsService _systemActivityLogsService;

        public TransactionController(ITransactionService transactionService, ISystemStatusService statusService, IWalletService walletService, ISMSService smsService, IMapper mapper, IEmailSender emailSender, ICashFlowService cashFlowService, IUserDetailService userDetailService, IFirebaseNotification firebaseNotification, IUserDeviceService userDeviceService, ISystemActivityLogsService systemActivityLogsService) 
        {
            _transactionService = transactionService;
            _walletService = walletService;
            _statusService = statusService;
            _smsService = smsService;
            _mapper = mapper;
            _emailSender = emailSender;
            _cashFlowService = cashFlowService;
            _userDetailService = userDetailService;
            _firebaseNotification = firebaseNotification;
            _userDeviceService = userDeviceService;
            _systemActivityLogsService = systemActivityLogsService;
        }

        [HttpPost("History")]
        public async Task<IActionResult> GetTransactionHistoryByWalletId([FromBody] TransactionPageRequest request)
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
        public async Task<IActionResult> GetTransactionById(long transactionId)
        {
            var transaction = await _transactionService.GetTransactionsById(transactionId);
            var transactionResource = _mapper.Map<Transaction, TransactionHistoryResource>(transaction);
            return Ok(transactionResource);
        }

        [HttpGet("{userId}/{currency}/{isMonthly}")]
        public async Task<IActionResult> GetTransactionAmount(string userId, int currency, bool isMonthly)
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
            var systemStatus = await _statusService.GetSystemStatus();
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

            if ((aplicationUserDetail.UserRoles.FirstOrDefault().Role.Name == "Member" && systemStatus.Status && walletBalance.ApplicationUser.UserDetail.Status == (int)Status.Suspend) && (transactionRequest.TransferTypeId == (int)TransferTypes.WleeTransfer))
            {
                return BadRequest(new{
                    status = 400,
                    message = "Member cannot make transaction when account is suspended. He/she can only cashout wallet balances." });
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
                SendSmsNotificationToSystemAdmin(newTransaction);

            }
            return Ok(newTransactionResource);
        }

        [SystemLock]
        [AllowAnonymous]
        [HttpPost("request")]
        public async Task<IActionResult> CreatePaymentRequest([FromBody] CreateTransactionResource transactionRequest)
        {
            var validator = new CreateTransactionValidator();
            var validationResult = await validator.ValidateAsync(transactionRequest);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var systemStatus = await _statusService.GetSystemStatus();

            var transactionToCreate = _mapper.Map<CreateTransactionResource, Transaction>(transactionRequest);

            var getUserByWalletId = await _walletService.GetUserDetailsByWalletIdAndCurrency(transactionRequest.ToWalletId);

            var aplicationUserDetail = await _userDetailService.GetApplicationUserById(getUserByWalletId.ApplicationUser.Id);

            if ((getUserByWalletId.ApplicationUser.UserRoles.FirstOrDefault().Role.Name == "Agent" && systemStatus.Status && getUserByWalletId.ApplicationUser.UserDetail.Status == (int)Status.Suspend) && (transactionToCreate.TransferTypeId == (int)TransferTypes.FillAgent))
            {
                return BadRequest(new {status = 400,
                    message = "Agent cannot make transaction when account is suspended. He/she can only cashout wallet balances." });
            }

            if ((getUserByWalletId.ApplicationUser.UserRoles.FirstOrDefault().Role.Name == "Member" && systemStatus.Status && getUserByWalletId.ApplicationUser.UserDetail.Status == (int)Status.Suspend) && (transactionToCreate.TransferTypeId == (int)TransferTypes.TopUp || transactionToCreate.TransferTypeId == (int)TransferTypes.WleeTransfer))
            {
                return BadRequest( new { status = 400,
                    message = "Member cannot make transaction when account is suspended. He/she can only cashout wallet balances." });
            }

            var newTransaction = await _transactionService.InitiateTransactionRequest(transactionToCreate);

            var newTransactionResource = _mapper.Map<Transaction, TransactionResource>(newTransaction);
            if (newTransactionResource != null)
            {
                await SendTransactionRequestActionSMS(newTransaction);
            }

            return Ok(newTransactionResource);
        }

        [HttpPut("approve")]
        [AllowAnonymous]
        public async Task<IActionResult> ApprovePaymentRequest(long paymentRequestId)
        {
            if (paymentRequestId <= 0)
                return BadRequest("Please enter valid payment request Id");

            var transactionToApprove = await _transactionService.GetTransactionsById(paymentRequestId);
            if (transactionToApprove == null)
                return BadRequest("Invalid payment Id");

            var approvedTransaction = new Transaction();

            if (transactionToApprove.TransferTypeId == TransferTypes.CashOut.GetHashCode())
            {
                approvedTransaction = await _transactionService.InitiateCashout(transactionToApprove);
            }
            else if (transactionToApprove.TransferTypeId == TransferTypes.MemberCashOutByWleePay.GetHashCode())
            {
                approvedTransaction = await _transactionService.InitiateCashOutWithWleePay(transactionToApprove);
            }
            else
                approvedTransaction = await _transactionService.InitiateTransaction(transactionToApprove);
           
            var newTransactionResource = _mapper.Map<Transaction, TransactionResource>(approvedTransaction);

            await LogTransaction(approvedTransaction, "approved");

            //send sms to user
            if (newTransactionResource != null)
            {
                await SendTransactionActionSMS(approvedTransaction);
                SendSmsNotificationToSystemAdmin(approvedTransaction);

            }


            return Ok(newTransactionResource);
        }

        [HttpPost("BulkApprove")]
        public async Task<IActionResult> BulkApprovePaymentRequest(List<long> paymentRequestIds)
        {
            TransactionResource newTransactionResource = new TransactionResource();

            foreach (var paymentRequestId in paymentRequestIds)
            {
                if (paymentRequestIds.Count() <= 0)
                    return BadRequest("Please enter valid payment request Id");

                var transactionToApprove = await _transactionService.GetTransactionsById(paymentRequestId);
                if (transactionToApprove == null)
                    return BadRequest("Invalid payment Id");
                var approvedTransaction = new Transaction();

                if (transactionToApprove.TransferTypeId == TransferTypes.CashOut.GetHashCode())
                {
                    approvedTransaction = await _transactionService.InitiateCashout(transactionToApprove);
                }
                else if (transactionToApprove.TransferTypeId == TransferTypes.MemberCashOutByWleePay.GetHashCode())
                {
                    approvedTransaction = await _transactionService.InitiateCashOutWithWleePay(transactionToApprove);
                }
                else
                    approvedTransaction = await _transactionService.InitiateTransaction(transactionToApprove);

                newTransactionResource = _mapper.Map<Transaction, TransactionResource>(approvedTransaction);

                await LogTransaction(approvedTransaction, "approved");

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
            var transactionResource = _mapper.Map<Transaction, TransactionResource>(result);
            if (result == null)
                return BadRequest("Invalid payment Id");

            await LogTransaction(transactionToReject, "rejected");

            //send sms to user
            if (result != null)
                await SendTransactionActionSMS(transactionToReject);

            return Ok(transactionResource);
        }

        [HttpPost("bulkReject")]
        public async Task<IActionResult> BulkRejectPaymentRequest(List<long> paymentRequestIds)
        {

            if (paymentRequestIds.Count() <= 0)
                return BadRequest("Please enter valid payment request Id");

            var updatedTransactions = await ChangePaymentRequestStatusInBulk(paymentRequestIds, Core.Enums.TransactionStatus.Rejected);
            if (updatedTransactions.Count() <= 0)
                return BadRequest("Invalid payment Id");

            //send sms to user
            if (updatedTransactions.Count() >= 0)
                foreach (var transaction in updatedTransactions)
                {
                    await SendTransactionActionSMS(transaction);
                }


            return Ok(updatedTransactions);
        }

        [HttpPut("cancel")]
        public async Task<IActionResult> CancelPaymentRequest(long paymentRequestId)
        {
            if (paymentRequestId <= 0)
                return BadRequest("Please enter valid payment request Id");

            var result = await ChangePaymentRequestStatus(paymentRequestId, Core.Enums.TransactionStatus.Cancel);
            var transactionResource = _mapper.Map<Transaction, TransactionResource>(result);
            await LogTransaction(result, "cancelled");

            if (result == null)
                return BadRequest("Invalid payment Id");

            return Ok(transactionResource);
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

        [HttpPost("HistoryByStatusAndType")]
        public async Task<IActionResult> GetTransactionHistoryByStatusTypeAndUserId(TransactionHistoryRequest transactionHistoryRequest)
        {
            var transactionResult = await _transactionService.GetTransactionsHistoryByUserIdAndType(transactionHistoryRequest.UserId,transactionHistoryRequest.Status, transactionHistoryRequest.TransferTypeId,transactionHistoryRequest.Index, transactionHistoryRequest.Size);
          
            
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

        [HttpPost("PaymentRequestHistory")]
        public async Task<IActionResult> GetIncomingOutGoingTopUpRequests([FromBody] AgentPaymentRequest paymentRequestParam)
        {
            var paymentRequest = await _transactionService.GetPaymentRequestByStatusAndType(paymentRequestParam.Type, paymentRequestParam.TransferStatus, paymentRequestParam.TransferId ,paymentRequestParam.PageSize, paymentRequestParam.PageIndex);

            if (paymentRequest == null)
                return Ok(new List<TransactionResource>());

            var transactionResource = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionHistoryResource>>(paymentRequest.Transactions);
           
            foreach (var item in transactionResource)
            {
                var time = Helpers.Common.ConvertISTtoLiberiaTime(item.CreatedDate);
                item.CreatedDate = time;
            }

            return Ok(new
            {
                transactionResource = transactionResource,
                totalCount = paymentRequest.TotalCount
            });
        }

        #region Helper function
        private async Task<Transaction> ChangePaymentRequestStatus(long paymentRequestId, Core.Enums.TransactionStatus status)
        {

            var transactionToUpdate = await _transactionService.GetTransactionsById(paymentRequestId);
            if (transactionToUpdate == null)
                return null;

            var updatedTransaction = await _transactionService.UpdateTransactionStatus(paymentRequestId, status);
            return updatedTransaction;
        }

        private async Task<IEnumerable<Transaction>> ChangePaymentRequestStatusInBulk(IEnumerable<long> paymentRequestId, Core.Enums.TransactionStatus status)
        {
            var rejectedTransaction = await _transactionService.UpdateTransactionStatusInBulk(paymentRequestId, status);
            return rejectedTransaction;
        }

        private async Task<string> SendTransactionActionSMS(Transaction transaction)
        {
            var userWallet = new Wallet();
            string messageText = string.Empty;
            string result = string.Empty;

            var status = (Core.Enums.TransactionStatus)transaction.Status;
            //Get from wallet user details
            var fromWalletUser = await _walletService.GetUserDetailsByWalletIdAndCurrency((long)transaction.FromWalletId);
            //Get to wallet user details
            var toWalletUser = await _walletService.GetUserDetailsByWalletIdAndCurrency((long)transaction.ToWalletId);

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
                transaction.TransferTypeId == Core.Enums.TransferTypes.FillAgent.GetHashCode() || 
                transaction.TransferTypeId == Core.Enums.TransferTypes.MemberTopUpByWleePay.GetHashCode())
            {
                var device = await _userDeviceService.GetUserDeviceByUserId(toWalletUser.ApplicationUser.UserDetail.UserId);

                foreach (var item in device)
                {
                    firebaseMessageRequest.DeviceId = item.DeviceId;
                    firebaseMessageRequest.DeviceToken = item.FcmToken;
                    firebaseMessageRequest.Body = "Top up request approved  for $" + transaction.Amount;
                    //var sendNotification = _firebaseNotification.SendFirebaseMessageRequest(firebaseMessageRequest);
                    try
                    {
                        var sendNotification = _firebaseNotification.SendFirebaseMessageRequest(firebaseMessageRequest);
                    }
                    catch (AggregateException ex)
                    {
                        foreach (var innerEx in ex.InnerExceptions)
                        {
                            if (innerEx is FirebaseMessagingException fme)
                            {
                                // Handle Firebase specific exceptions
                                Console.WriteLine($"Firebase error: {fme.Message}");
                            }
                            else
                            {
                                Console.WriteLine($"General error: {innerEx.Message}");
                            }
                        }
                    }
                }
                 userWallet = (status == Core.Enums.TransactionStatus.Pending) ? fromWalletUser : toWalletUser;
                string name = (status == Core.Enums.TransactionStatus.Pending)
                    ? $"{toWalletUser.ApplicationUser.UserDetail.FirstName} {toWalletUser.ApplicationUser.UserDetail.LastName}"
                    : $"{fromWalletUser.ApplicationUser.UserDetail.FirstName} {fromWalletUser.ApplicationUser.UserDetail.LastName}";
                messageText = GenerateMessageText(transaction.Amount, fromWalletUser.Currency, name, transaction.Id, status, transaction.TransferTypeId);
            }
            else if (transaction.TransferTypeId == Core.Enums.TransferTypes.CashOut.GetHashCode() || 
                     transaction.TransferTypeId == Core.Enums.TransferTypes.AgentCommissionCashout.GetHashCode() ||
                     transaction.TransferTypeId == Core.Enums.TransferTypes.AgentCashout.GetHashCode() || 
                     transaction.TransferTypeId == Core.Enums.TransferTypes.MemberCashOutByWleePay.GetHashCode())
            {
                var device = await _userDeviceService.GetUserDeviceByUserId(toWalletUser.ApplicationUser.UserDetail.UserId);

                foreach (var item in device)
                {
                    firebaseMessageRequest.DeviceId = item.DeviceId;
                    firebaseMessageRequest.DeviceToken = item.FcmToken;
                    firebaseMessageRequest.Body = "Cash out request approved for $" + transaction.Amount;

                    //var sendNotification = _firebaseNotification.SendFirebaseMessageRequest(firebaseMessageRequest);
                    try
                    {
                        var sendNotification = _firebaseNotification.SendFirebaseMessageRequest(firebaseMessageRequest);
                    }
                    catch (AggregateException ex)
                    {
                        foreach (var innerEx in ex.InnerExceptions)
                        {
                            if (innerEx is FirebaseMessagingException fme)
                            {
                                // Handle Firebase specific exceptions
                                Console.WriteLine($"Firebase error: {fme.Message}");
                            }
                            else
                            {
                                Console.WriteLine($"General error: {innerEx.Message}");
                            }
                        }
                    }
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

                    //var sendNotification = _firebaseNotification.SendFirebaseMessageRequest(firebaseMessageRequest);
                    try
                    {
                        var sendNotification = _firebaseNotification.SendFirebaseMessageRequest(firebaseMessageRequest);
                    }
                    catch (AggregateException ex)
                    {
                        foreach (var innerEx in ex.InnerExceptions)
                        {
                            if (innerEx is FirebaseMessagingException fme)
                            {
                                // Handle Firebase specific exceptions
                                Console.WriteLine($"Firebase error: {fme.Message}");
                            }
                            else
                            {
                                Console.WriteLine($"General error: {innerEx.Message}");
                            }
                        }
                    }
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
            var fromWalletUser = await _walletService.GetUserDetailsByWalletIdAndCurrency((long)transaction.FromWalletId);
            //Get to wallet user details
            var toWalletUser = await _walletService.GetUserDetailsByWalletIdAndCurrency((long)transaction.ToWalletId);

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
            var fromWalletUser = await _walletService.GetUserDetailsByWalletIdAndCurrency((long)transaction.FromWalletId);

            //Get to wallet user details
            var toWalletUser = await _walletService.GetUserDetailsByWalletIdAndCurrency((long)transaction.ToWalletId);

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

        private string GetClientIpAddress()
        {
            if (HttpContext.Connection.RemoteIpAddress != null)
            {
                return HttpContext.Connection.RemoteIpAddress.ToString();
            }
            return "Unknown";
        }

        private async Task<bool> LogTransaction(Transaction transactionToApprove, string requestStatus)
        {
            SystemActivityLog systemActivityLog = new SystemActivityLog();
            systemActivityLog.UserId = transactionToApprove.FromWallet.UserId;
            systemActivityLog.Email = transactionToApprove.FromWallet.ApplicationUser.Email;
            systemActivityLog.IpAddress = GetClientIpAddress();
            systemActivityLog.Action = ActionsForSystemLogs.Transaction.ToDescriptionString();

            if (transactionToApprove.TransferTypeId == TransferTypes.CashOut.GetHashCode() || transactionToApprove.TransferTypeId == TransferTypes.MemberCashOutByWleePay.GetHashCode())
            {
                systemActivityLog.Description = "Transaction request is successfully "+ requestStatus + " for member cashout";
            }
            else if (transactionToApprove.TransferTypeId == TransferTypes.TopUp.GetHashCode()|| transactionToApprove.TransferTypeId == TransferTypes.FillAgent.GetHashCode() || transactionToApprove.TransferTypeId == TransferTypes.MemberTopUpByWleePay.GetHashCode())
            {
                systemActivityLog.Description = "Transaction request is successfully "+ requestStatus + " for topup";

            }
            else if (transactionToApprove.TransferTypeId == TransferTypes.AgentCashout.GetHashCode() || transactionToApprove.TransferTypeId == TransferTypes.AgentCommissionCashout.GetHashCode())
            {
                systemActivityLog.Description = "Transaction request is successfully "+ requestStatus + " for agent cashout";

            }
            else
            {
                systemActivityLog.Description = "payment transfer request is successfully "+ requestStatus ;

            }

            var addLogs = await _systemActivityLogsService.AddSystemLogs(systemActivityLog);
            if (addLogs != null) 
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        
        private async void SendSmsNotificationToSystemAdmin(Transaction transaction)
        {
            string messageText = string.Empty;
            string result = string.Empty;
            
            var systemAdmins = await _userDetailService.GetAllUsersByRole(Role.SystemAdmin.GetHashCode());
            
            string amountString = transaction.Amount.ToString("C2", new CultureInfo("en-US"));
            string currencyName = transaction.FromWallet.Currency.Symbol == "L$" ? "LRD" : "USD";
            messageText = MessagesTemplates.TransactionCompleted(amountString, currencyName, transaction.Id);


            foreach (var admin in systemAdmins)
            {
                result = _smsService.sendMessage(messageText, admin.UserDetail.PhoneNumber);
            }
        }
        #endregion

    }
}
