using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using System.Threading.Tasks;
using System.Web.Http;
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


        public StripeController(IConfiguration configuration, IStripeService stripeService)
        {
            _configuration = configuration;
            _stripeService = stripeService;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> Create([Microsoft.AspNetCore.Mvc.FromBody] PaymentRequestForStripe paymentRequest)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var options = new ChargeCreateOptions
            {
                Amount = 50, // amount in cents
                Currency = "usd",
                Description = "Test Charge",
                Source = paymentRequest.Token
            };

            var service = new ChargeService();
            Charge charge = await service.CreateAsync(options);

            return Ok(new { success = true, charge });
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
        public string Token { get; set; }
    }

    public class CreatePaymentLink
    {
        public double Amount { get; set; }
    }
}

