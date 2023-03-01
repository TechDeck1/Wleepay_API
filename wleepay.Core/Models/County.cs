using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class County
    {
        public County() {
            UserDetails = new Collection<UserDetail>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
