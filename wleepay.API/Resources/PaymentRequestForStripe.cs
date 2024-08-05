namespace wleepay.API.Resources
{
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
        public string? AccountId { get; set; }
        public ExtraParameters? ExtraParameters { get; set; }

    }

    public class CreatePaymentLink
    {
        public double Amount { get; set; }
    }
}
