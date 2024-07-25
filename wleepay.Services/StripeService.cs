using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class StripeService : IStripeService
    {
        private readonly string _apiKey;

        public StripeService(string apiKey)
        {
            _apiKey = apiKey;
            StripeConfiguration.ApiKey = _apiKey;
        }
        public async Task<string> CreatePaymentLinkAsync(decimal amount, string currency = "usd")
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
            {
                "card",
            },
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = currency,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Test Product",
                        },
                        UnitAmountDecimal = amount * 100, // amount in cents
                    },
                    Quantity = 1,
                },
            },
                Mode = "payment",
                SuccessUrl = "https://yourdomain.com/success",
                CancelUrl = "https://yourdomain.com/cancel",
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);
            return session.Url;
        }
    }
    
}
