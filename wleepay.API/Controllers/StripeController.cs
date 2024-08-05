using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Stripe;
using System.Threading.Tasks;
using System.Web.Http;
using wleepay.API.Resources;
using wleepay.Communication.Interface;
using wleepay.Communication.Services;
using wleepay.Core.Common;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Services;

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

        public StripeController(IConfiguration configuration, IStripeService stripeService, ISochitelTransactionService sochitelTransactionService, IMapper mapper, IExternalApiService externalApiService, ISMSService sMSService, ISystemActivityLogsService systemActivityLogsService, IUserDetailService userDetailService)
        {
            _configuration = configuration;
            _stripeService = stripeService;
            _sochitelTransactionService = sochitelTransactionService;
            _mapper = mapper;
            _externalApiService = externalApiService;
            _sMSService = sMSService;
            _systemActivityLogsService = systemActivityLogsService;
            _userDetailService = userDetailService;
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

                    SochitelTransactions sochitelTransactions = new SochitelTransactions
                    {
                        TransactionId = "123456",
                        TransactionType = "Mobile Recharge",
                        TransactionAmount = paymentRequest.Amount,
                        CommissionRate = 0,
                        OperatorId = paymentRequest.Operator,
                        ProductId = paymentRequest.ProductId,
                        PhoneNumber = paymentRequest.Msisdn,
                        UserReference = exeTransactionToCreate.UserReference,

                    };

                    var sTansaction = _sochitelTransactionService.AddSochitelTransactions(sochitelTransactions);

                    _sMSService.sendMessage("mobile recharge is successfully done", paymentRequest.Msisdn);

                    var userDetail = await _userDetailService.GetUserAccountByPhone(paymentRequest.Msisdn);
                   
                    SystemActivityLog systemActivityLog = new SystemActivityLog();
                    systemActivityLog.UserId = userDetail.Id;
                    systemActivityLog.Email = userDetail.Email;
                    systemActivityLog.IpAddress = GetClientIpAddress();
                    systemActivityLog.Action = ActionsForSystemLogs.MobileRecharge.ToDescriptionString();
                    systemActivityLog.Description = GenericMessagesForSystemLogs.MobileRecharge.ToDescriptionString();
                    var addLogs = await _systemActivityLogsService.AddSystemLogs(systemActivityLog);

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

                    SochitelTransactions sochitelTransactions = new SochitelTransactions
                    {
                        TransactionId = "123456",
                        TransactionType = "Bill Payment",
                        TransactionAmount = paymentRequest.Amount,
                        CommissionRate = 0,
                        OperatorId = paymentRequest.Operator,
                        ProductId = paymentRequest.ProductId,
                        PhoneNumber = paymentRequest.Msisdn,
                        UserReference = exeTransactionToCreate.UserReference,

                    };

                    var sTansaction = _sochitelTransactionService.AddSochitelTransactions(sochitelTransactions);

                    _sMSService.sendMessage("bill payment is successfully done", paymentRequest.Msisdn);

                    var userDetail = await _userDetailService.GetUserAccountByPhone(paymentRequest.Msisdn);
                    SystemActivityLog systemActivityLog = new SystemActivityLog();
                    systemActivityLog.UserId = userDetail.Id;
                    systemActivityLog.Email = userDetail.Email;
                    systemActivityLog.IpAddress = GetClientIpAddress();
                    systemActivityLog.Action = ActionsForSystemLogs.BillPayment.ToDescriptionString();
                    systemActivityLog.Description = GenericMessagesForSystemLogs.BillPayment.ToDescriptionString();
                    var addLogs = await _systemActivityLogsService.AddSystemLogs(systemActivityLog);

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

