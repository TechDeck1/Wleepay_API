using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface IUsersStaffRoleRepository : IRepository<UsersStaffRole>
    {
        Task<UsersStaffRole> AddUserStaffRole(UsersStaffRole usersStaffRole);

    }
}
