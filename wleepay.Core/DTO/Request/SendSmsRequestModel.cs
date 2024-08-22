using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.DTO.Request
{
    public class SendSmsRequestModel
    {
        public string Note { get; set; }
        public string? PhoneNumber {  get; set; }
    }

    public class SendEmailSmsAlertModel
    {
        public string Note { get; set; }
        public string AlertType { get; set; }
        public string SendTo { get; set; }
    }
}
