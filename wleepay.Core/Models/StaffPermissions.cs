using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class StaffPermissions
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int PermissionId { get; set; }
        public string Permissions { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public virtual StaffPermissionType PermissionType { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
