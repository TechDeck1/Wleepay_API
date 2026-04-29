using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace wleepay.Core.Models
{
    public class WleeSendUser
    {
        public virtual ICollection<WleeSendUserDetails> WleeSendUserDetails { get; set; }
        public virtual Address Address { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}
    