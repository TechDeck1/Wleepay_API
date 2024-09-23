using System;

namespace wleepay.API.Resources
{
    public class SochitelTransactionViewModel
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public long? WalletTransactionId { get; set; }
        public int? CreditCardTransactionId { get; set; }
        public string TransactionType { get; set; }
        public string TransactionAmount { get; set; }
        public string CommissionRate { get; set; }
        public string PhoneNumber { get; set; }
        public string OperatorId { get; set; }
        public string ProductId { get; set; }
        public string ExtraInfo { get; set; }
        public string UserReference { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string MeterNumber { get; set; }
        public string MobileVendor { get; set; }
        public string ProductName { get; set; }
        public ExtraInfo ParsedExtraInfo { get; set; }
    }
}
