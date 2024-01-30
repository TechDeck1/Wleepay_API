using System.Collections.Generic;

namespace wleepay.API.Resources
{
    public class StaffPermissionsResponseResource
    {
        public UserDetailResource UserDetail { get; set; }
        public IEnumerable<StaffPermissionResource> StaffPermissions { get; set; }

    }
}
