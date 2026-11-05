using Stripe;
using Stripe.Checkout;
using System.Threading.Tasks;
using wleepay.Core.DTO.Request;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;

namespace wleepay.Core.DTO.Request
{
    public static class RechargeFlows
    {
        public const string Mobile = "mobile";
        public const string Lec = "lec";
        public const string Voucher = "voucher";
    }

    public class RechargeCheckoutContext
    {
        public string Flow { get; set; }                       // RechargeFlows.*
        public string UserId { get; set; }                     // optional: the endpoints are anonymous
        public string CustomerEmail { get; set; }               // authenticated email claim for Stripe prefill
        public string ReceiverPhoneNumber { get; set; }
        public int TransferTypeId { get; set; }
        public RechargeRequestDetails Recharge { get; set; }   // Command, Operator, Msisdn, ProductId, Amount, Simulate
        public string ProductName { get; set; }                // voucher receipt
        public string AccountId { get; set; }                  // LEC: meter number
        public decimal AmountOperator { get; set; }            // LEC: amount before the service fee
        public bool NativePaymentSheet { get; set; }
    }
}

namespace wleepay.Core.Services
{
    public interface IStripeService
    {
        // One method for all flows. Normal payment when `recharge` is null.
        Task<CheckoutSessionResponse> CreateEmbeddedCheckoutSessionAsync(CreateCheckoutSessionRequest request, string apiKey = null, RechargeCheckoutContext recharge = null);
        Task<Session> GetCheckoutSessionAsync(string sessionId);
        Task<(Session Session, bool PaymentConfirmed)> GetCheckoutSessionStatusAsync(string sessionId);
        Task<MobilePaymentIntentResponse> CreateMobilePaymentIntentAsync(decimal amount, RechargeCheckoutContext recharge, string apiKey = null);

        // Called by the webhook background processor.
        Task HandleCheckoutCompletedAsync(Session session);
        Task HandlePaymentIntentAuthorizedAsync(PaymentIntent paymentIntent);
        Task HandlePaymentFailedAsync(PaymentIntent paymentIntent);
    }
}
