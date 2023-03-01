using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class WalletType
    {
        public WalletType() {
            Wallets = new Collection<Wallet>();
            TransferTypes = new Collection<TransferType>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Wallet> Wallets { get; set; }
        public virtual ICollection<TransferType> TransferTypes { get; set; }
    }
}
