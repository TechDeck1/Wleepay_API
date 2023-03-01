using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class UpdateContactResource
    {
        public string UserId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailId { get; set; }
    }
}
