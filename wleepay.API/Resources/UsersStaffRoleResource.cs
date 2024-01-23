using System;
using wleepay.Core.Models;

namespace wleepay.API.Resources
{
    public class UsersStaffRoleResource
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int StaffRoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual StaffRoleResource StaffRoles { get; set; }
    }
}
