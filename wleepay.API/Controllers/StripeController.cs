using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Stripe;
using System.Threading.Tasks;
using System.Web.Http;
using wleepay.API.Resources;
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



        public StripeController(IConfiguration configuration, IStripeService stripeService, ISochitelTransactionService sochitelTransactionService, IMapper mapper, IExternalApiService externalApiService)
        {
            _configuration = configuration;
            _stripeService = stripeService;
            _sochitelTransactionService = sochitelTransactionService;
            _mapper = mapper;
            _externalApiService = externalApiService;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> CreatePaymentForMobileRecharge([Microsoft.AspNetCore.Mvc.FromBody] PaymentRequestForStripe paymentRequest)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var options = new ChargeCreateOptions
            {
                Amount = paymentRequest.Amount * 100, // amount in cents
                Currency = "usd",
                Description = "Test Charge",
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

             return Ok(new { success = true, charge });


            }
            return BadRequest("payment failed");

        }


        [Microsoft.AspNetCore.Mvc.HttpPost("billPayment")]
        public async Task<IActionResult> CreatePaymentForBillPayment([Microsoft.AspNetCore.Mvc.FromBody] PaymentRequestForStripe paymentRequest)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var options = new ChargeCreateOptions
            {
                Amount = paymentRequest.Amount * 100, // amount in cents
                Currency = "usd",
                Description = "Test Charge",
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
            return BadRequest("payment failed");

        }


        [Microsoft.AspNetCore.Mvc.HttpPost("createPaymentLink")]
        [System.Web.Http.AllowAnonymous]
        public async Task<IActionResult> CreatePaymentLink(CreatePaymentLink createPaymentLink)
        {
            string apiKey = _configuration["Stripe:SecretKey"];
            var paymentLink = await _stripeService.CreatePaymentLinkAsync(createPaymentLink.Amount, apiKey);
            return Ok(new { url = paymentLink });
        }
    }

    public class PaymentRequestForStripe
    {
        public SochitelAuth Auth { get; set; }
        public string Version { get; set; }
        public string Command { get; set; }
        public string Operator { get; set; }
        public string ProductId { get; set; }
        public string Msisdn { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public long Amount { get; set; }
        public string Token { get; set; }
        public string AccountId {  get; set; }
        public ExtraParameters? ExtraParameters { get; set; }

    }

    public class CreatePaymentLink
    {
        public double Amount { get; set; }
    }
}

