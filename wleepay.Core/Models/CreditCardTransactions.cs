using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class CreditCardTransactions
    {
        public CreditCardTransactions()
        {
            SochitelTransactions = new Collection<SochitelTransactions>();

        }
        public int Id { get; set; } 
        public double Amount { get; set; }
        public string StripeTransactionId { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public string ExtraInfo { get; set; }
        public string BalanceTransactionId { get; set; }
        public DateTime CreatedDate { get; set; }


        public ICollection<SochitelTransactions> SochitelTransactions { get; set; }


    }
}
