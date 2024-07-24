using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.DTO.Request
{
    public class SochitelRequestPayload
    {
        public Auth Auth { get; set; }
        public int Version { get; set; }
        public string Command { get; set; }
    }

    public class Auth
    {
        public string Username { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
    }
}
