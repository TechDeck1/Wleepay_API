using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class PortalLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


    public class LockAccount 
    {
        public string Email { get; set; }

    }

    public class SendCode
    {
        public string Phone { get; set; }
        public string UserId { get; set; }

    }

    public class VerificationCodeSentResponse
    {
        public string Success { get; set; }
        public string Message { get; set; }

    }



}
