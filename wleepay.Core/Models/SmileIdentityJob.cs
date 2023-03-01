using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class SmileIdentityJob
    {
        public string Id { get; set; }
        public string SecKey { get; set; }
        public string UserId { get; set; }
        public string PartnerId { get; set; }
        public string ApplicationUserId { get; set; }
        public string LastStatus { get; set; }
        public string TimeStamp { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
