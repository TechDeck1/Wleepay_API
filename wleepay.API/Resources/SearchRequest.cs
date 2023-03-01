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
}
