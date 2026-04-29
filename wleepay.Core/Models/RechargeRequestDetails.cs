using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.Models
{
    public class RechargeRequestDetails
    {
        public string username { get; set; }
        public string salt { get; set; }
        public string password { get; set; }
        public string Operator { get; set; }
        public string ProductId { get; set; }           // For mobile

        public string Msisdn { get; set; }       // For mobile
        public string Simulate { get; set; }     
        public string? UserReference { get; set; }        // For mobile
        public decimal Amount { get; set; }              // For mobile
        public string Command { get; set; }             




    }
}
