using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.Core.Common
{
    public class AuthResult
    {
        public string UserId { get; set; }

        public bool IsLoggedInVerified { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
