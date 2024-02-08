using System;
using wleepay.Core.Models;

namespace wleepay.API.Resources
{
    public class StaffPermissionAndRoleResource
    {
        public string EmailId { get; set; }
        public int StaffRoleId { get; set; }
        public string Permissions { get; set; }

    }
}
