using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using wleepay.API.Resources;
using wleepay.Communication.EmailTemplates;
using wleepay.Communication.Interface;
using wleepay.Communication.Services;
using wleepay.Core.Common;
using wleepay.Core.DTO.Request;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Services;
using static wleepay.Communication.EmailTemplates.EmailTemplates;

namespace wleepay.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class StripeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IStripeService _stripeService;
        private readonly ISochitelTransactionService _sochitelTransactionService;
        private readonly IMapper _mapper;
        private readonly IExternalApiService _externalApiService;
        private readonly ISMSService _sMSService;
        private readonly ISystemActivityLogsService _systemActivityLogsService;
        private readonly IUserDetailService _userDetailService;
        private readonly IUserDeviceService _userDeviceService;
        private readonly IFirebaseNotification _firebaseNotification;
        private readonly ICreditCardTransactionService _creditCardTransactionService;
        private readonly IEmailSender _emailSender;

        public StripeController(IConfiguration configuration, IStripeService stripeService, ISochitelTransactionService sochitelTransactionService, IMapper mapper, IExternalApiService externalApiService, ISMSService sMSService, ISystemActivityLogsService systemActivityLogsService, IUserDetailService userDetailService, IUserDeviceService userDeviceService, IFirebaseNotification firebaseNotification, ICreditCardTransactionService creditCardTransactionService, IEmailSender emailSender)
        {
            _configuration = configuration;
            _stripeService = stripeService;
            _sochitelTransactionService = sochitelTransactionService;
            _mapper = mapper;
            _externalApiService = externalApiService;
            _sMSService = sMSService;
            _systemActivityLogsService = systemActivityLogsService;
            _userDetailService = userDetailService;
            _userDeviceService = userDeviceService;
            _firebaseNotification = firebaseNotification;
            _creditCardTransactionService = creditCardTransactionService;
            _emailSender = emailSender;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> CreatePaymentForMobileRecharge([Microsoft.AspNetCore.Mvc.FromBody] PaymentRequestForStripe paymentRequest)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var options = new ChargeCreateOptions
            {
                Amount = paymentRequest.Amount * 100, // amount in cents
                Currency = "usd",
                Description = "Mobile recharge",
                Source = paymentRequest.Token
            };

            var exeTransactionToCreate = _mapper.Map<PaymentRequestForStripe, SochitelTransactionRequest>(paymentRequest);
            exeTransactionToCreate.Simulate = "1";
            exeTransactionToCreate.UserReference = "12345";

            var result = await _externalApiService.PostDataAsync<SochitelTransactionRequest>(exeTransactionToCreate);

            SochitelTransactionResponse transactionResponse = JsonConvert.DeserializeObject<SochitelTransactionResponse>(result);

            if (transactionResponse.Status.Name == "Successful")
            {
                var service = new ChargeService();

                Charge charge = await service.CreateAsync(options);

                if (charge.Status == "succeeded")
                {
                    CreditCardTransactions creditTransactions = new CreditCardTransactions
                    {
                        Amount = paymentRequest.Amount,
                        StripeTransactionId = charge.Id,
                        Status = charge.Status,
                        PaymentMethod = charge.PaymentMethod,
                        BalanceTransactionId = charge.BalanceTransactionId,
                        CreatedDate = DateTime.UtcNow,
                        ExtraInfo = "Mobile Recharge"

                    };
                    var creditCardTransactions = await _creditCardTransactionService.AddCreditCardTransactions(creditTransactions);

                    SochitelTransactions sochitelTransactions = new SochitelTransactions
                    {
                        TransactionId = "123456",
                        TransactionType = "Mobile Recharge",
                        CreditCardTransactionId = creditCardTransactions.Id,
                        TransactionAmount = paymentRequest.Amount,
                        CommissionRate = 0,
                        OperatorId = paymentRequest.Operator,
                        ProductId = paymentRequest.ProductId,
                        PhoneNumber = paymentRequest.Msisdn,
                        UserReference = exeTransactionToCreate.UserReference,

                    };

                    var sTansaction = _sochitelTransactionService.AddSochitelTransactions(sochitelTransactions);

                    _sMSService.sendMessage("mobile recharge is successfully done", paymentRequest.Msisdn);


                    EmailHtmlTemplates emailHtml = new EmailHtmlTemplates();
                    var template = emailHtml.EmailTemplateForBillPaymentAndRecharge(charge.ReceiptUrl,"User");
                    _emailSender.SendEmailAsync("ariel.ranjot@gmail.com", template, "User", "Receipt for mobile recharge");

                    var userDetail = await _userDetailService.GetUserAccountByPhone(paymentRequest.Msisdn);
                   
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
                            firebaseMessageRequest.Body = "Mobile recharge approved for $" + charge.Amount;
                            firebaseMessageRequest.Title = "Mobile Recharge";
                            var sendNotification = _firebaseNotification.SendFirebaseMessageRequest(firebaseMessageRequest);
                        }
                    }

                    return Ok(new
                    {
                        success = true,
                        amount = charge.Amount,
                        stripeTransactionId = charge.Id,
                        receiptUrl = charge.ReceiptUrl,
                        transactionResponse
                    });
                }

                return BadRequest("payment is failed");
            }
            return Ok(transactionResponse);

        }


        [Microsoft.AspNetCore.Mvc.HttpPost("billPayment")]
        public async Task<IActionResult> CreatePaymentForBillPayment([Microsoft.AspNetCore.Mvc.FromBody] PaymentRequestForStripe paymentRequest)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var options = new ChargeCreateOptions
            {
                Amount = paymentRequest.Amount * 100, // amount in cents
                Currency = "usd",
                Description = "Bill payment",
                Source = paymentRequest.Token
            };

            var exeTransactionToCreate = _mapper.Map<PaymentRequestForStripe, SochitelBillPaymentTransactionRequest>(paymentRequest);
            exeTransactionToCreate.Simulate = "1";
            exeTransactionToCreate.UserReference = "12345";

            var result = await _externalApiService.PostDataAsync<SochitelBillPaymentTransactionRequest>(exeTransactionToCreate);

            SochitelTransactionResponse transactionResponse = JsonConvert.DeserializeObject<SochitelTransactionResponse>(result);

            if (transactionResponse.Status.Name == "Successful")
            {
                var service = new ChargeService();

                Charge charge = await service.CreateAsync(options);
                if (charge.Status == "succeeded")
                {

                    CreditCardTransactions creditTransactions = new CreditCardTransactions
                    {
                        Amount = paymentRequest.Amount,
                        StripeTransactionId = charge.Id,
                        Status = charge.Status,
                        PaymentMethod = charge.PaymentMethod,
                        BalanceTransactionId = charge.BalanceTransactionId,
                        CreatedDate = DateTime.UtcNow,
                        ExtraInfo = "Bill Payment"

                    };
                    var creditCardTransactions = await _creditCardTransactionService.AddCreditCardTransactions(creditTransactions);


                    SochitelTransactions sochitelTransactions = new SochitelTransactions
                    {
                        TransactionId = "123456",
                        TransactionType = "Bill Payment",
                        CreditCardTransactionId = creditCardTransactions.Id,
                        TransactionAmount = paymentRequest.Amount,
                        CommissionRate = 0,
                        OperatorId = paymentRequest.Operator,
                        ProductId = paymentRequest.ProductId,
                        PhoneNumber = paymentRequest.Msisdn,
                        UserReference = exeTransactionToCreate.UserReference,

                    };

                    var sTansaction = _sochitelTransactionService.AddSochitelTransactions(sochitelTransactions);

                    _sMSService.sendMessage("bill payment is successfully done", paymentRequest.Msisdn);

                    EmailHtmlTemplates emailHtml = new EmailHtmlTemplates();
                    var template = emailHtml.EmailTemplateForBillPaymentAndRecharge(charge.ReceiptUrl, "User");
                    _emailSender.SendEmailAsync("ariel.ranjot@gmail.com", template, "User", "Receipt for bill payment");

                    var userDetail = await _userDetailService.GetUserAccountByPhone(paymentRequest.Msisdn);
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
                            firebaseMessageRequest.Body = "Bill payment approved for $" + charge.Amount;
                            firebaseMessageRequest.Title = "Bill Payment";
                            var sendNotification = _firebaseNotification.SendFirebaseMessageRequest(firebaseMessageRequest);
                        }
                    }
                    return Ok(new { 
                        success = true, 
                        amount = charge.Amount,
                        stripeTransactionId = charge.Id,
                        receiptUrl=charge.ReceiptUrl,
                        transactionResponse
                    });
                }

                return BadRequest("Payment is failed.");

            }
            
            return Ok(transactionResponse);

        }


        [Microsoft.AspNetCore.Mvc.HttpPost("createPaymentLink")]
        [System.Web.Http.AllowAnonymous]
        public async Task<IActionResult> CreatePaymentLink(CreatePaymentLink createPaymentLink)
        {
            string apiKey = _configuration["Stripe:SecretKey"];
            var paymentLink = await _stripeService.CreatePaymentLinkAsync(createPaymentLink.Amount, apiKey);
            return Ok(new { url = paymentLink });
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

