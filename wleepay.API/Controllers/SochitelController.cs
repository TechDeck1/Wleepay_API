using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using wleepay.Communication.Interface;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Storage.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using IEmailSender = wleepay.Communication.Interface.IEmailSender;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.Services;
using System.Net.Http;
using System.Text;
using wleepay.Core.DTO.Request;
using wleepay.Core.Enums;
using System.Linq;
using AutoMapper.Internal;
using System;
using Microsoft.SqlServer.Management.Smo;
using wleepay.Core.Common;
using FirebaseAdmin.Messaging;
using wleepay.Communication.Templates;
using Microsoft.IdentityModel.Tokens;
using wleepay.Communication.Services;
using wleepay.API.Helpers;
using Stripe;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class SochitelController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserDetailService _userDetailsService;
        private readonly ISMSService _smsService;
        private readonly IWalletService _walletService;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public readonly  IHostingEnvironment _hostingEnvironment;
        private readonly IEmailSender _emailSender;
        private readonly ISystemActivityLogsService _systemActivityLogsService;
        private readonly ISMSService _SMSService;
        private readonly IExternalApiService _externalApiService;
        private readonly ITransactionService _transactionService;
        private readonly ISochitelTransactionService _sochitelTransactionService;
        private readonly IFirebaseNotification _firebaseNotification;
        private readonly IUserDeviceService _userDeviceService;

        public SochitelController(UserManager<ApplicationUser> userManager,
            IUserDetailService userDetailsService,
            IWalletService walletService,
            ILogger<AccountController> logger,
            ISMSService smsService,
            IConfiguration configuration,
            IMapper mapper,
            IHostingEnvironment hostingEnvironment,
            IEmailSender emailSender,
            ISystemActivityLogsService systemActivityLogsService,
            ISMSService sMSService,
            IExternalApiService externalApiService,
            ITransactionService transactionService,
            ISochitelTransactionService sochitelTransactionService,
            IFirebaseNotification firebaseNotification,
            IUserDeviceService userDeviceService)
        {
            _userManager = userManager;
            _userDetailsService = userDetailsService;
            _walletService = walletService;
            _logger = logger;
            _smsService = smsService;
            _configuration = configuration;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _emailSender = emailSender;
            _systemActivityLogsService = systemActivityLogsService;
            _SMSService = sMSService;
            _externalApiService = externalApiService;
            _transactionService = transactionService;
            _sochitelTransactionService = sochitelTransactionService;
            _firebaseNotification = firebaseNotification;
            _userDeviceService = userDeviceService;
        }

        [HttpPost]
        [Route("operators")]
        public async Task<IActionResult> GetOperators([FromBody] SochitelOperatorRequest sochitelOperatorRequest)
        {
            try
            {
                var result = await _externalApiService.PostDataAsync<SochitelOperatorRequest>(sochitelOperatorRequest);

                var products = JsonConvert.DeserializeObject<SochitelOperatorResponse>(result);

                List<Operator> operators = new List<Operator>();
                foreach (var kvp in products.Result)
                {
                    var product = kvp.Value;
                    operators.Add(product);
                }

                var newOperatorList = operators;

                return Ok(new {
                    status = new { 
                        id=products.Status.Id,
                        type = products.Status.Type,
                        typeName = products.Status.TypeName,
                        name = products.Status.Name,    
                    },
                    command = products.Command,
                    timestamp = products.Timestamp,
                    reference = products.Reference,
                    result= newOperatorList
                });
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [Route("operatorProducts")]
        public async Task<IActionResult> GetOperatorProducts([FromBody] SochitelProductOperatorRequest sochitelProductOperatorRequest)
        {
            try
            {
                var result = await _externalApiService.PostDataAsync<SochitelProductOperatorRequest>(sochitelProductOperatorRequest);

                var products = JsonConvert.DeserializeObject<SochitelOperatorProductsResponse>(result);

                List<wleepay.API.Resources.Product> operatorsProduct = new List<wleepay.API.Resources.Product>();

                foreach (var product in products.Result.Products)
                {
                    operatorsProduct.Add(product.Value);
                }

                var newOperatorProductList = operatorsProduct;

                return Ok(new
                {
                    status = new
                    {
                        id = products.Status.Id,
                        type = products.Status.Type,
                        typeName = products.Status.TypeName,
                        name = products.Status.Name,
                    },
                    command = products.Command,
                    timestamp = products.Timestamp,
                    reference = products.Reference,
                    result = newOperatorProductList
                });
            }
            catch(Exception ex) 
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [Route("execTransaction")]
        public async Task<IActionResult> execTransaction([FromBody]ExecTransactionRequest execTransactionRequest)
        {
            try
            {
                if (execTransactionRequest.FromWalletId != null)
                {
                    var fromWalletData = await _walletService.GetWallet(execTransactionRequest.FromWalletId);
                    if (fromWalletData == null) 
                    {
                        return BadRequest("Wallet not found.");
                    }
                    bool checkValidBalance = false;
                    if (fromWalletData.CurrencyId == (int)Currencies.LRD)
                    {
                        if (fromWalletData.Balance > execTransactionRequest.AmountInLd)
                        {
                            checkValidBalance = true;
                        }
                        else
                        {
                            checkValidBalance = false;
                            return BadRequest("There is no enough balance in wallet to make transaction");
                        }

                    }
                    if (fromWalletData.CurrencyId == (int)Currencies.USD)
                    {

                        if (fromWalletData.Balance > execTransactionRequest.AmountInUsd)
                            checkValidBalance = true;
                        else
                        {
                            checkValidBalance = false;
                            return BadRequest("There is no enough balance in wallet to make transaction");
                        }
                    }

                    if (checkValidBalance)
                    {

                        var exeTransactionToCreate = _mapper.Map<ExecTransactionRequest, SochitelTransactionRequest>(execTransactionRequest);
                        exeTransactionToCreate.Simulate = "0";

                        var result = await _externalApiService.PostDataAsync<SochitelTransactionRequest>(exeTransactionToCreate);

                        SochitelTransactionResponse transactionResponse = JsonConvert.DeserializeObject<SochitelTransactionResponse>(result);

                        if (transactionResponse.Status.Name == "Successful")
                        {
                            var walletData = await _walletService.GetWalletByWalletTypeId((int)WalletTypes.SystemWallet);
                            var walletListResource = _mapper.Map<IEnumerable<Wallet>, IEnumerable<WalletResource>>(walletData);
                            WalletResource walletResource = new WalletResource();
                            foreach (var wallet in walletListResource)
                            {
                                if (wallet != null && wallet.CurrencyId == Currencies.USD.GetHashCode())
                                {
                                    walletResource = wallet;
                                }
                                else
                                {
                                    walletResource = null;
                                }
                            }

                            var transactionToCreate = new Transaction()
                            {
                                ToWalletId = (long)(walletResource?.Id),
                                FromWalletId = execTransactionRequest.FromWalletId,
                                Amount = (double)execTransactionRequest.AmountInUsd,
                                TransferTypeId = (int)TransferTypes.MobileRechargeTransfer,
                                Description = "Mobile recharge transfer to system wallet",
                                CreatedDate = System.DateTime.UtcNow,
                                Status = (int)wleepay.Core.Enums.TransactionStatus.Approved,

                            };

                            var newTransaction = await _transactionService.InitiateTransaction(transactionToCreate);

                            SochitelTransactions sochitelTransactions = new SochitelTransactions
                            {
                                TransactionId = transactionResponse?.Result?.Id ?? "12345",
                                TransactionType = "Mobile Recharge",
                                WalletTransactionId = newTransaction.Id,
                                TransactionAmount = newTransaction.Amount.ToString(),
                                CommissionRate = (transactionResponse?.Result?.Balance?.Commission == null ? "0" : transactionResponse?.Result?.Balance?.Commission),
                                OperatorId = execTransactionRequest.Operator,
                                ProductId = execTransactionRequest.ProductId,
                                PhoneNumber = execTransactionRequest.Msisdn,
                                UserReference = exeTransactionToCreate.UserReference,
                                CreatedDate = DateTime.UtcNow


                            };

                            var sTansaction = _sochitelTransactionService.AddSochitelTransactions(sochitelTransactions);
                            var message = MessagesTemplates.AirtimeAndLecPaymentTemplate(transactionResponse.Result.Operator.Name, transactionResponse.Result.Id, transactionResponse.Reference.ToString(), DateTime.UtcNow.ToString(),newTransaction.Amount.ToString());

                            _smsService.sendMessage(message, execTransactionRequest.Msisdn);

                            var userDetail = await _userDetailsService.GetUserAccountByPhone(execTransactionRequest.Msisdn);
                            SystemActivityLog systemActivityLog = new SystemActivityLog();
                            systemActivityLog.UserId = userDetail.Id;
                            systemActivityLog.Email = userDetail.Email;
                            systemActivityLog.IpAddress = GetClientIpAddress();
                            systemActivityLog.Action = ActionsForSystemLogs.MobileRecharge.ToDescriptionString();
                            systemActivityLog.Description = GenericMessagesForSystemLogs.MobileRecharge.ToDescriptionString();
                            var addLogs = await _systemActivityLogsService.AddSystemLogs(systemActivityLog);

                            FirebaseMessageRequest firebaseMessageRequest = new FirebaseMessageRequest();

                            var device = await _userDeviceService.GetUserDeviceByUserId(userDetail.Id);
                            if (device != null)
                            {
                                foreach (var item in device)
                                {
                                    firebaseMessageRequest.DeviceId = item.DeviceId;
                                    firebaseMessageRequest.DeviceToken = item.FcmToken;
                                    firebaseMessageRequest.Body = "Mobile recharge approved for $" + newTransaction.Amount;
                                    firebaseMessageRequest.Title = "Mobile Recharge";
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
                            }

                            return Ok(transactionResponse);

                        }

                        return Ok(transactionResponse);
                    }
                    
                }

                return BadRequest("Wallet Id should be valid");

            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("billPaymentTransaction")]
        public async Task<IActionResult> billPaymentTransaction([FromBody] BillPaymentTransactionRequest execTransactionRequest)
        {
            try
            {
                if (execTransactionRequest.FromWalletId != null)
                {
                    var fromWalletData = await _walletService.GetWallet(execTransactionRequest.FromWalletId);
                    if (fromWalletData == null)
                    {
                        return BadRequest("Wallet not found.");
                    }
                    bool checkValidBalance = false;
                    if(fromWalletData.CurrencyId == (int)Currencies.LRD) 
                    {
                        if (fromWalletData.Balance > execTransactionRequest.AmountInLd)
                        {
                            checkValidBalance = true;
                        }
                        else
                        {
                            checkValidBalance = false;
                            return BadRequest("There is no enough balance in wallet to make transaction");
                        }

                    }
                    if (fromWalletData.CurrencyId == (int)Currencies.USD)
                    {

                        if (fromWalletData.Balance > execTransactionRequest.AmountInUsd)
                            checkValidBalance = true;
                        else 
                        {
                            checkValidBalance = false;
                            return BadRequest("There is no enough balance in wallet to make transaction");
                        }
                    }

                    if (checkValidBalance)
                    {
                    
                        var exeTransactionToCreate = _mapper.Map<BillPaymentTransactionRequest, SochitelBillPaymentTransactionRequest>(execTransactionRequest);
                        exeTransactionToCreate.Simulate = "0";
                        //exeTransactionToCreate.UserReference = execTransactionRequest.FromWalletId.ToString();

                        var result = await _externalApiService.PostDataAsync<SochitelBillPaymentTransactionRequest>(exeTransactionToCreate);

                        SochitelTransactionResponse transactionResponse = JsonConvert.DeserializeObject<SochitelTransactionResponse>(result);

                        if (transactionResponse.Status.Name == ResponseStatusForSochitel.Successful.ToDescriptionString())
                        {
                            var walletData = await _walletService.GetWalletByWalletTypeId((int)WalletTypes.SystemWallet);
                            var walletListResource = _mapper.Map<IEnumerable<Wallet>, IEnumerable<WalletResource>>(walletData);
                            WalletResource walletResources = new WalletResource();
                            foreach (var walletResource in walletListResource)
                            {
                                if (walletResource != null && walletResource.CurrencyId == execTransactionRequest.Currency)
                                {
                                    walletResources = walletResource;
                                }
                                else
                                {
                                    walletResources = null;
                                }
                            }
                            var transactionToCreate = new Transaction()
                            {
                                ToWalletId = (long)(walletResources?.Id),
                                FromWalletId = execTransactionRequest.FromWalletId,
                                Amount = (double)execTransactionRequest.AmountInUsd,
                                TransferTypeId = (int)TransferTypes.BillPayment,
                                Description = "bill payment transfer to system wallet",
                                CreatedDate = System.DateTime.UtcNow,
                                Status = (int)wleepay.Core.Enums.TransactionStatus.Approved,

                            };

                            var newTransaction = await _transactionService.InitiateTransaction(transactionToCreate);

                            SochitelTransactions sochitelTransactions = new SochitelTransactions
                            {
                                TransactionId = transactionResponse?.Result?.Id ?? "12345",
                                TransactionType = "Bill Payment",
                                WalletTransactionId = newTransaction.Id,
                                TransactionAmount = newTransaction.Amount.ToString(),
                                CommissionRate = (transactionResponse?.Result?.Balance?.Commission == null ? "0" : transactionResponse?.Result?.Balance?.Commission),
                                OperatorId = execTransactionRequest.Operator,
                                ProductId = execTransactionRequest.ProductId,
                                PhoneNumber = execTransactionRequest.Msisdn,
                                UserReference = transactionResponse.Reference,
                                CreatedDate = DateTime.UtcNow,
                                ExtraInfo = transactionResponse.Result.Instructions,


                            };

                            InstructionDetails instructionDetails = new InstructionDetails();
                            if (!transactionResponse.Result.Instructions.IsNullOrEmpty())
                            {
                                instructionDetails = Common.DeserializeInstructions(transactionResponse.Result.Instructions);
                            }

                            var message = MessagesTemplates.LecCreditPurchaseReceiptTemplate(instructionDetails.Amount.ToString(), instructionDetails.Units.ToString(), instructionDetails.Token, instructionDetails.AccountNumber, instructionDetails.CustomerName, transactionResponse.Result.Id, instructionDetails.ReceiptNumber , DateTime.UtcNow.ToString());
                            _smsService.sendMessage(message, execTransactionRequest.Msisdn);
                           
                            var sTansaction = _sochitelTransactionService.AddSochitelTransactions(sochitelTransactions);

                            var userDetail = await _userDetailsService.GetUserAccountByPhone(execTransactionRequest.Msisdn);
                           
                            SystemActivityLog systemActivityLog = new SystemActivityLog();
                            systemActivityLog.UserId = userDetail.Id;
                            systemActivityLog.Email = userDetail.Email;
                            systemActivityLog.IpAddress = GetClientIpAddress();
                            systemActivityLog.Action = ActionsForSystemLogs.BillPayment.ToDescriptionString();
                            systemActivityLog.Description = GenericMessagesForSystemLogs.BillPayment.ToDescriptionString();
                            var addLogs = await _systemActivityLogsService.AddSystemLogs(systemActivityLog);

                            FirebaseMessageRequest firebaseMessageRequest = new FirebaseMessageRequest();

                            var device = await _userDeviceService.GetUserDeviceByUserId(userDetail.Id);
                            if (device != null)
                            {
                                foreach (var item in device)
                                {
                                    firebaseMessageRequest.DeviceId = item.DeviceId;
                                    firebaseMessageRequest.DeviceToken = item.FcmToken;
                                    firebaseMessageRequest.Body = "Bill payment approved for $" + newTransaction.Amount;
                                    firebaseMessageRequest.Title = "Bill Payment";

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
                            }

                            return Ok(transactionResponse);
                        }

                        if(transactionResponse.Status.Name == ResponseStatusForSochitel.InvalidDestination.ToDescriptionString())
                        {
                            transactionResponse.Status.Name = Messages.MeterNumberIncorrect.ToDescriptionString();
                            return Ok(transactionResponse);
                        }

                        return Ok(transactionResponse);
                    }
                }

                return BadRequest("Wallet Id should be valid");

            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [Route("getBalance")]
        public async Task<IActionResult> GetBalance([FromBody] SochitelGetBalanceRequest sochitelGetBalanceRequest)
        {
            try
            {
                var result = await _externalApiService.PostDataAsync<SochitelGetBalanceRequest>(sochitelGetBalanceRequest);

                var balanceResponse = JsonConvert.DeserializeObject<SochitelBalanceResponse>(result);

                return Ok(balanceResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        #region helper methods
        private string GetClientIpAddress()
        {
            if (HttpContext.Connection.RemoteIpAddress != null)
            {
                return HttpContext.Connection.RemoteIpAddress.ToString();
            }
            return "Unknown";
        }
        #endregion

    }
}
