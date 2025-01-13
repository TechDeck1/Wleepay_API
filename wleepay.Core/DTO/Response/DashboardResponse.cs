using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.DTO.Response
{
    public class DashboardResponse
    {
        public int TotalSubscribers { get; set; }
        public int ActiveSubscribers { get; set; }
        public decimal RetentionRate { get; set; }
        public decimal ChurnRate { get; set; }
        public decimal ActiveToday { get; set; }
        public decimal ActiveLast7Days { get; set; }
        public decimal ActiveLast30Days { get; set; }
        public int InactiveLast30Days { get; set; }
        public int InactivePrevious30Days { get; set; }
        public double TotalSubscribersWalletBalanceByLD { get; set; }

        public double TotalSubscribersWalletBalanceByUSD { get; set; }
        public double TotalAgentsWalletBalanceByLD { get; set; }
        public double TotalAgentsWalletBalanceByUSD { get; set; }

        public int SignUpSubscriberLast7Days {get;set;}
        public int SignUpSubscriberLast30Days {get;set;}
        public int PendingKYCSubscriber { get;set;}
    }

    public class EarnedCommission
    {
        public double MobileDataCreditCommission { get; set; }
        public double LECCreditCommission { get; set; }
    }

    public class PayBillCreditPurchaseCount
    {
        public double MobileDataPurchaseCount {get; set; }
        public double LECPurchaseCount { get; set; }
    }
}
