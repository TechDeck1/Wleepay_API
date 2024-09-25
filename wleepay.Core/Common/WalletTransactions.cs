using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Core.Common
{
    public class WalletTransactions
    {
        public Transaction AgentCashout { get; set; }
        public Transaction taxAmount { get; set; }
        public Transaction commissionTransaction { get; set; }
        public Transaction organizationFeeAmount { get; set; }
    }
}

