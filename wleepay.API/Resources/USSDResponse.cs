using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class USSDResponse
    {
        public string text { get; set; }
        public string phoneNumber { get; set; }
        public string sessionId { get; set; }
        public string serviceCode { get; set; }
    }
}
