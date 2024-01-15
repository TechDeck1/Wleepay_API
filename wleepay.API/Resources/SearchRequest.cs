using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class SearchRequest
    {
        public string LoginName { get; set; }
        public string PhoneNumber { get; set; }
        public Core.Enums.Role? Role { get; set; }
    }

    public class SearchRequestByEmail
    {
        public string Email { get; set; }
        public Core.Enums.Role Role { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
    }

    public class SearchRequestForVerification
    {
        public string PhoneNumber { get; set; }
    }
}
