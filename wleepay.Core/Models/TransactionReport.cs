using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.Models
{
    public class TransactionReport
    {
        public string TransactionId { get; set; }
        public string Name { get; set; }  
        public string Address { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public string ReceiverNumber { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
