using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class TransactionResource
    {
        public long Id { get; set; }
        public long ToWalletId { get; set; }
        public long FromWalletId { get; set; }
        public float Amount { get; set; }
        public int TransferTypeId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ParentId { get; set; }
        public int? Status { get; set; }

        public string TransactionNumber
        {
            get
            {
                return this.Id.ToString("0000000000000000");
            }
        }
        public virtual TransferTypeResource TransferType { get; set; }
        public virtual LookupResource TransactionStatus { get; set; }
        public virtual CurrencyResource Currency { get; set; }
    }
}
