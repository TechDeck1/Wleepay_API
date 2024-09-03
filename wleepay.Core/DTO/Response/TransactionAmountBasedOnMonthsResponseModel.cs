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
        public string Name { get; set; }
        public int Value { get; set; }
        public double? TotalAmount { get; set; }  // Total transaction amount for this type
        public string? FormatedTotalAmount { get; set; }  // Total transaction amount for this type

    }
}
