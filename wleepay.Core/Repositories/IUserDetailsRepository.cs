using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface IUserDetailsRepository:IRepository<UserDetail>
    {
        Task<UserDetail> GetUserDetailsById(string userId);
        Task<ApplicationUser> GetApplicationUser(string userId = "", string name="", string phoneNumber="", bool fetchWallet = false, bool fetchKYCDoc = false, int role = 0);
        Task<bool> IsPhoneAlreadyRegistered(string phone);
        Task<bool> IsAccountAlreadyExists(string accountNumber);
        Task<UserDetail> GetUserDetailsByPhone(string phone);
        Task<IEnumerable<ApplicationUser>> GetUserDetailsByRole(int role);
        Task<IEnumerable<ApplicationUser>> GetUsers(Core.Enums.Role role, int pageIndex = 0, int pageSize = 10);
    }
}
