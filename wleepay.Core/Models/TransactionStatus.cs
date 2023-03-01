using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class TransactionStatus
    {
        public TransactionStatus() {
            Transactions = new Collection<Transaction>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
