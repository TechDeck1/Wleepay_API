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
        public string? FormatedTotalAmount { get; set; }  // Total transaction amount for this type

    }

    public class TransactionSummaryResponseModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int Currency { get; set; }
        public double SubscriberCashouts { get; set; }
        public double SubscriberTopUps { get; set; }
        public double AgentWalletTopup { get; set; }
        public double AgentCommissionCashout { get; set; }
        public double PhoneCreditPurchased { get; set; }
        public double LECCreditPurchased { get; set; }
        public int TotalTransactionCount { get; set; }
    }

}
