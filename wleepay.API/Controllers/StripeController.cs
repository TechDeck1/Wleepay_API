using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using System.Threading.Tasks;
using wleepay.Core.Services;
using wleepay.Services;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentRequestForStripe paymentRequest)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var options = new ChargeCreateOptions
            {
                Amount = 5000, // amount in cents
                Currency = "usd",
                Description = "Test Charge",
                Source = paymentRequest.Token
            };

            var service = new ChargeService();
            Charge charge = await service.CreateAsync(options);

            return Ok(new { success = true, charge });
        }


        [HttpPost("create-payment-link")]
        public async Task<IActionResult> CreatePaymentLink(decimal amount)
        {
            var paymentLink = await _stripeService.CreatePaymentLinkAsync(amount);
            return Ok(new { url = paymentLink });
        }
    }

    public class PaymentRequestForStripe
    {
        public string Token { get; set; }
    }
}

