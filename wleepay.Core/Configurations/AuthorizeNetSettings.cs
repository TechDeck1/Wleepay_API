using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.Configurations
{
     public class AuthorizeNetSettings
     {
        public string ApiLoginId { get; set; }
        public string TransactionKey { get; set; }
        public string Environment { get; set; }

    }

    public class SochitelSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
