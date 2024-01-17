using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Core.DTO.Response
{
    public class CustomerServiceResponseResource
    {
        public int TotalCount { get; set; }

        public IEnumerable<CustomerTickets> CustomerTickets { get; set; }
    }
}
