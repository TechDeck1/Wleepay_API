namespace wleepay.Core.DTO.Request
{
    public class CreateCheckoutSessionRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "usd";
        public string ProductName { get; set; } = "Mobile Airtime Top-Up";
        public string TaxCode { get; set; } = "txcode_80000000";
        public string PromotionCodeId { get; set; }
        public string ReturnUrl { get; set; }
        public bool HostedCheckout { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
    }

    // CheckoutSessionResponse.cs
    public class CheckoutSessionResponse
    {
        public bool Success { get; set; }
        public string ClientSecret { get; set; }
        public string SessionId { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
    }

    public class MobilePaymentIntentResponse
    {
        public bool Success { get; set; }
        public string PaymentIntentClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public string PublishableKey { get; set; }
        public string Message { get; set; }
    }

    // StripeCheckoutSettings.cs (Optional - for configuration)
    public class StripeCheckoutSettings
    {
        public string SecretKey { get; set; }
        public string BaseUrl { get; set; }
        public string UiMode { get; set; } = "embedded_page";
        public string Mode { get; set; } = "payment";
        public string BillingAddressCollection { get; set; } = "required";
    }
}
