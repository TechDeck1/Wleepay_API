using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.DTO.Response
{
    public class AgentSubscribersCountResponseModel
    {
        public int AgentCount { get; set; }
        public int SubscribersCount { get; set; }

    }

    public class CountByMonths
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class subscribersByType
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
