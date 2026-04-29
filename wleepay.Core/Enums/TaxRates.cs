using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace wleepay.Core.Enums
{
    public enum TaxRates
    {
        [Description("agent commision")] AgentCommisionRate,
        [Description("cash out fee")] CashOutFeeRate,
        [Description("tax fee")] TaxFeeRate
    }
}
