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
       
        public StripeService()
        {
        }
        public async Task<string> CreatePaymentLinkAsync(double amount, string apiKey, string currency = "usd")
        {
            StripeConfiguration.ApiKey = apiKey;

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
                        UnitAmount = (long)amount * 100, // amount in cents
                    },
                    Quantity = 1,
                },
            },
                Mode = "payment",
                SuccessUrl = "http://localhost:4200/#/payments/BuyMobileAirtime",
                CancelUrl = "http://localhost:4200/#/payments/BuyMobileAirtime",
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);
            return session.Url;
        }
    }
    
}
