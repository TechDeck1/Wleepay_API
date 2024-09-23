using System;

namespace wleepay.API.Resources
{
    public class ExtraInfo
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Tax { get; set; }
        public decimal Units { get; set; }
        public string ReceiptNumber { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }

        public TokenInfo Token { get; set; }
    }

    public class TokenInfo
    {
        public string TokenType { get; set; }
        public string Token { get; set; }
    }

}
