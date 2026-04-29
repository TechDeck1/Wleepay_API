using System;
using wleepay.Core.Models;

namespace wleepay.API.Resources
{
    public class SochitelTransactionCommissionEarnedResource
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
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public virtual TransactionResource Transaction { get; set; }
    }

}

