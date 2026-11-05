using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.Checkout;
using wleepay.Core.Services;

namespace wleepay.Services
{

    public class WebhookService : IWebhookService
    {
        private readonly IStripeService _stripe;
        private readonly ILogger<WebhookService> _logger;

        public WebhookService(IStripeService recharge, ILogger<WebhookService> logger)
        {
            _stripe = recharge;
            _logger = logger;
        }

        public async Task ProcessWebhookAsync(string json)
        {
            var stripeEvent = EventUtility.ParseEvent(json, throwOnApiVersionMismatch: false);

            switch (stripeEvent.Type)
            {
                case "payment_intent.amount_capturable_updated":
                    if (stripeEvent.Data.Object is PaymentIntent authorized)
                        await _stripe.HandlePaymentIntentAuthorizedAsync(authorized);
                    else
                        _logger.LogWarning("Event {Id}: object is not a PaymentIntent.", stripeEvent.Id);
                    break;

                case "checkout.session.completed":
                    {
                        if (!(stripeEvent.Data.Object is Session session))
                        {
                            _logger.LogWarning("Event {Id}: object is not a Checkout Session.", stripeEvent.Id);
                            break;
                        }

                        await _stripe.HandleCheckoutCompletedAsync(session);
                        break;
                    }

                case "payment_intent.payment_failed":
                    {
                        if (!(stripeEvent.Data.Object is PaymentIntent paymentIntent))
                        {
                            _logger.LogWarning("Event {Id}: object is not a PaymentIntent.", stripeEvent.Id);
                            break;
                        }

                        await _stripe.HandlePaymentFailedAsync(paymentIntent);
                        break;
                    }

                default:
                    _logger.LogInformation("Ignored Stripe event {Type} ({Id}).", stripeEvent.Type, stripeEvent.Id);
                    break;
            }
        }
    }
}
