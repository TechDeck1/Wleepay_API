using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.Models
{
    public class SystemStatus
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
