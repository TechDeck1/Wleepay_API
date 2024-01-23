using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class StaffPermissionService : IStaffPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StaffPermissionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<StaffPermissions> AddStaffPermissions(StaffPermissions permission)
        {

            await _unitOfWork.StaffPermissions.AddAsync(permission);
            await _unitOfWork.CommitAsync();
            return permission;
        }
    }
}
