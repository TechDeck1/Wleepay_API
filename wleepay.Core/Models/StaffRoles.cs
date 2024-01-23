using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class StaffRoles
    {
        public StaffRoles()
        {
            UsersStaffRole = new Collection<UsersStaffRole>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UsersStaffRole> UsersStaffRole { get; set; }

    }
}
