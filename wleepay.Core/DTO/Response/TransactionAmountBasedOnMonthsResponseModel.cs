using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.DTO.Response
{
    public class TransactionAmountBasedOnMonthsResponseModel
    {
        public string MonthName {  get; set; }
        public double TotalAmount_LD { get; set; }
        public double TotalAmount_USD { get; set; }

        public int TotalCount { get; set; }
    }

    public class TransactionCountByTypeResponseModel
    {
        public string Type { get; set; }
        public int TotalCount { get; set; }
    }
}
