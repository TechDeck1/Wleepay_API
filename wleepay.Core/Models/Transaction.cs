using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class Transaction
    {
        public Transaction() 
        {
            SochitelTransactions = new Collection<SochitelTransactions>();

        }
        public long Id { get; set; }
        public long? ToWalletId { get; set; }
        public long? FromWalletId { get; set; }
        public double Amount { get; set; }
        public int TransferTypeId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ParentId { get; set; }
        public int? Status { get; set; }

        public virtual Wallet ToWallet { get; set; }
        public virtual Wallet FromWallet { get; set; }
        public virtual TransferType TransferType { get; set; }
        public virtual TransactionStatus TransactionStatus { get; set; }
        public virtual ICollection<SochitelTransactions> SochitelTransactions { get; set; }

    }
}
