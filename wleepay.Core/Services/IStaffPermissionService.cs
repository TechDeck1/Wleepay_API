using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface IStaffPermissionService
    {
        Task<StaffPermissions> AddStaffPermissions(StaffPermissions permission);
    }
}
