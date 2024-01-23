using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class UsersStaffRole
    {
        public UsersStaffRole() 
        {
            StaffRoles= new Collection<StaffRoles>();
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public int StaffRoleId { get; set; }
        public virtual ICollection<StaffRoles> StaffRoles { get; set; }

    }
}
