using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Core.DTO.Response
{
    public class CashFlowHistoryResponse
    {
        public int TotalCount { get; set; }
        public IEnumerable<CashFlowHistory> CashFlowHistory { get; set; }
    }
}
