using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.Core.Enums;

namespace wleepay.API.Resources
{
    public class TransactionReportRequest : PagingRequest
    {
        public TransferTypes TransferType { get; set; }
    }
}
