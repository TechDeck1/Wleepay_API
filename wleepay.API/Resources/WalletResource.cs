using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace wleepay.API.Resources
{
    public class WalletResource
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public int CurrencyId { get; set; }
        public int Type { get; set; }
        public double Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public CurrencyResource Currency { get; set; }
        public WalletTypeResource WalletType { get; set; }
        public IEnumerable<TransactionResource> TransactionFrom { get; set; }
        public IEnumerable<TransactionResource> TransactionTo { get; set; }


    }
}
