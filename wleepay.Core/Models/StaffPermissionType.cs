using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class StaffPermissionType
    {
        public StaffPermissionType() 
        {
            StaffPermissions= new Collection<StaffPermissions>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Collection<StaffPermissions> StaffPermissions { get; set;}
    }
}
