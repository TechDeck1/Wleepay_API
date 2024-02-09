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
            if (usersStaffRole.UserId == null)
            {
                await _unitOfWork.UsersStaffRole.AddAsync(usersStaffRole);
                await _unitOfWork.CommitAsync();
            }
            //else
            //{
            //    await _unitOfWork.UsersStaffRole.
            //}
            return usersStaffRole;
        }
    }
}
