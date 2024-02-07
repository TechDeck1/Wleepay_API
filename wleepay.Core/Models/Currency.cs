using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class Currency
    {
        public Currency() {
            Wallets = new Collection<Wallet>();
            CashFlowHistory = new Collection<CashFlowHistory>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public virtual ICollection<Wallet> Wallets { get; set; }
        public virtual ICollection<CashFlowHistory> CashFlowHistory { get; set; }
    }
}
