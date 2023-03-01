using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class OTPRequest:LoginRequest
    {
    }

    public class documentKycImage
    {
        public string image { get; set; }
        public string userId { get; set; }
    }
}
