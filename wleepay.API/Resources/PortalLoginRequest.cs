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

    public class VerificationModel
    {
        public string Code { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }

    }

    public class SystemAdminVerificationModel
    {
        public string Code { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }

    }

    public class SendOTPForSystemAdmin
    {
        public string Phone { get; set; }
        public string UserId { get; set; }

    }


}
