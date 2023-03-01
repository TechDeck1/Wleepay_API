using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Core.DTO.Request
{
    public class UserInfo : UserDetail
    {
        public string Email { get; set; }      
        public string DocNumber { get; set; }
        public string DocType { get; set; }
        public int DocID { get; set; }
        public string Country { get; set; }
        public string KYCImage { get; set; }
    }
}
