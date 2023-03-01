using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace wleepay.Core.Models
{
    public partial class AspNetUserRole
    {
        [Key]
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public virtual AspNetRole Role { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
