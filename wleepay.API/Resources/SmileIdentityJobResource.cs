using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class SmileIdentityJobResource
    {
        public string Id { get; set; }
        public string SecKey { get; set; }
        public string UserId { get; set; }
        public string PartnerId { get; set; }
        public string ApplicationUserId { get; set; }
        public string LastStatus { get; set; }
        public string TimeStamp { get; set; }
    }
}
