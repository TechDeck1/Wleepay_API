using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
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
        Task<IEnumerable<ApplicationUser>> GetAllUsersByRole(int role);
        Task<UserDetailsResponseResource> GetUsers(Core.Enums.Role role, int pageIndex = 0, int pageSize = 10);
        Task<UserDetailsResponseResource> GetAllUsersById(Core.Enums.Role role, int pageIndex, int pageSize, string createdBy);
        Task<UserDetailsResponseResource> SearchApplicationUserByEmail(string searchText, Core.Enums.Role role, int index, int size);
        Task<TransactionAndUserResponseResource> VerifyUserAccountByPhone( string phoneNumber = "");
        Task<ApplicationUser> GetStaffPermissionsById(string userId);
        Task<UserDetail> AddDeviceIdAndToken(string deviceId, string token, string userId);
        Task<AgentSubscribersCountResponseModel> GetActiveAgentsAndSubscribersCount();
        Task<IList<CountByMonths>> GetActiveAgentsByMonth();
        Task<IList<CountByMonths>> GetActiveSubscribersByMonth();



    }
}
