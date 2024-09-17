using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace wleepay.Core.Models
{
    public class TransferType
    {
        public TransferType() {
            Transactions = new Collection<Transaction>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FromWalletType { get; set; }
        public int? ToWalletType { get; set; }
        public string Description { get; set; }
        public int? TransferFee { get; set; }
        public virtual TransferFee TransferFeeDetail { get; set; }
        

        public virtual WalletType FromWalletTypeDetail { get; set; }
        public virtual WalletType ToWalletTypeDetail { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
