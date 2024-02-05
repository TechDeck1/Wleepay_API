using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.Models
{
    public class CashFlowHistory
    {
        public int Id { get; set; }
        public long? WalletId { get; set; }
        public double? TotalAmount { get; set; }
        public double? DepositAmount { get; set; }
        public double? WithdrawAmount { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Wallet toWallet { get; set; }
        public virtual TransactionStatus TransactionStatus { get; set; }



    }
}
