using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class LoginRequest
    {
        
        public string Email { get; set; }
        public string Pin { get; set; }
    }
}
