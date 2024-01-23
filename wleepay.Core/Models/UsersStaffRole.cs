using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class UsersStaffRole
    {
       
        public int Id { get; set; }
        public string UserId { get; set; }
        public int StaffRoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual StaffRoles StaffRoles { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }



    }
}
