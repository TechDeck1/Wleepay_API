using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.DTO.Request
{
    public class SochitelAuth
    {
        public string username { get; set; }
        public string salt { get; set; }
        public string password { get; set; }
    }
}
