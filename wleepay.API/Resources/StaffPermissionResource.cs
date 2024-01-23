using System;
using wleepay.Core.Models;

namespace wleepay.API.Resources
{
    public class StaffPermissionResource
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int PermissionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public virtual StaffPermissionTypeResource PermissionType { get; set; }
    }
}
