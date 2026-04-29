using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.DTO.Response
{
    public class TransactionAmountBasedOnMonthsResponseModel
    {
        public string Name {  get; set; }
        public double TotalAmount_LD { get; set; }
        public double TotalAmount_USD { get; set; }

        public int Value { get; set; }
    }

    public class TransactionCountByTypeResponseModel
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public double? TotalAmount { get; set; }  // Total transaction amount for this type

    }

    public class TransactionSummaryResponseModel
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public double? TotalAmount { get; set; }
       
    }

}
