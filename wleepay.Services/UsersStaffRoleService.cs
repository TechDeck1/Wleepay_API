using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class UsersStaffRoleService : IUsersStaffRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersStaffRoleService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<UsersStaffRole> AddUserStaffRole(UsersStaffRole usersStaffRole)
        {
           var result = await _unitOfWork.UsersStaffRole.AddUserStaffRole(usersStaffRole);
            return result;
        }
        public async Task<UsersStaffRole> UpdateUserStaffRole(UsersStaffRole usersStaffRole)
        {
            var result = await _unitOfWork.UsersStaffRole.UpdateUserStaffRole(usersStaffRole);
            return result;
        }
    }
}
