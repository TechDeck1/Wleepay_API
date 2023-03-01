using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.Enums
{
    public enum TransactionStatus
    {
        Pending = 1,
        Rejected = 2,
        Approved = 3,
        Failed = 4,
        Cancel = 5
    }

}
