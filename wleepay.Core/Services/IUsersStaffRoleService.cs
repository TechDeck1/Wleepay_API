using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface IUsersStaffRoleService
    {
        Task<UsersStaffRole> AddUserStaffRole(UsersStaffRole usersStaffRole);
        Task<UsersStaffRole> UpdateUserStaffRole(UsersStaffRole usersStaffRole);

    }
}
