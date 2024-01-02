using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Core.DTO.Response
{
    public class ActivityResponseResource
    {
        public int TotalCount { get; set; }

        public List<SystemActivityLog> ActivityLogs { get; set; }
    }
}
