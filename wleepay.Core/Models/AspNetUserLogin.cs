using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace wleepay.Core.Models
{
    public partial class AspNetUserLogin
    {        
        [Key]
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
       
        public string UserId { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
