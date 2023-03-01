using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class Wallet
    {
        public Wallet() {
            TransactionTo = new Collection<Transaction>();
            TransactionFrom = new Collection<Transaction>();
            //TransferTypes = new Collection<TransferType>();
        }
        public long Id { get; set; }
        public string UserId { get; set; }
        public int CurrencyId { get; set; }
        public int Type { get; set; }
        public double Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual WalletType WalletType { get; set; }

        public virtual ICollection<Transaction> TransactionTo { get; set; }
        public virtual ICollection<Transaction> TransactionFrom { get; set; }
        //public virtual ICollection<TransferType> TransferTypes { get; set; }
    }
}
