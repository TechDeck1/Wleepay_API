using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace wleepay.Core.Models
{
    public partial class KYCStatus
    {
        public KYCStatus() {
            UserDetails = new Collection<UserDetail>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
