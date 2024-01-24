using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.Models
{
    public class Verification
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
        public string VerificationExpiry { get; set; }
        public DateTime CreatedDate { get; set;}

    }
}
