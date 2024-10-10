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
        public decimal activeToday { get; set; }
        public decimal activeLast7Days { get; set; }
        public decimal activeLast30Days { get; set; }
        public int InactiveLast30Days { get; set; }
        public int InactivePrevious30Days { get; set; }
    }
}
