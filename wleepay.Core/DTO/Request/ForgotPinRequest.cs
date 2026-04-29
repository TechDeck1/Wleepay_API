using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.DTO.Request
{
    public class ForgotPinRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
        public string NewPin { get; set; }
        public string ConfirmPin { get; set; }

        public string Mode { get; set; } //Email or Phone

    }
}
