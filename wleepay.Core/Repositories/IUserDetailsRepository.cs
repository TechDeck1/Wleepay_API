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
        Task<UserDetailsResponseResource> SearchApplicationUserByEmail(string searchText, Core.Enums.Role role, int index, int size, int filterType);
        Task<TransactionAndUserResponseResource> VerifyUserAccountByPhone( string phoneNumber = "");
        Task<ApplicationUser> GetStaffPermissionsById(string userId);
        Task<UserDetail> AddDeviceIdAndToken(string deviceId, string token, string userId);
        Task<AgentSubscribersCountResponseModel> GetActiveAgentsAndSubscribersCount();
        Task<decimal> GetEarnedCommission(string transactionType);
        Task<IList<CountByMonths>> GetActiveAgentsByMonth();
        Task<IList<CountByMonths>> GetActiveSubscribersByMonth();
        Task<IList<CountByMonths>> GetPendingKYCSubscribersByMonth();
        Task<IList<CountByMonths>> GetSubscribersSignup(DateTime daysCount);
        Task<IList<subscribersByType>> GetActiveSubscribersByType();
        Task<int> GetTotalSubscribersAsync();
        Task<double> GetTotalWalletBalanceByCurrency(int walletType,int currencyId);
        Task<int> GetActiveSubscribersAsync();
        Task<int> GetInactiveSubscribersAsync(DateTime since);
        Task<decimal> GetActiveSubscriberLast30DaysAsync();
        Task<decimal> GetSubscriberRetentionRateAsync();
        Task<decimal> GetActiveSubscriberTodayAsync();
        Task<decimal> GetActiveSubscriberLast7daysAsync();
        Task<decimal> GetSubscriberChurnRateAsync();
        Task<UserDetailsResponseResource> GetReportOfUserByStatusAsync(int role, int status , int pageIndex = 0, int pageSize = 10);
        Task<TicketsResponse> GetReportOfUserByOpenTicketsAsync( int role, int status, int pageIndex = 0, int pageSize = 10);
        Task<UserDetailsResponseResource> GetInActiveSubscriberAndMemberListAsync(int role);
        Task<UserDetailsResponseResource> GetActiveSubscriberAndMemberListAsync(int role);
        Task<UserDetailsResponseResource> GetSubscriberAndMemberListByWalletBalanceAsync(int WalletType , int role, double balance, int walletType);
        Task<UserDetailsResponseResource> GetReportBySubscribersKYCLevelAsync(int level , int role);
        Task<UserDetailsResponseResource> GetAllActiveAccountsAsync(int role);
        Task<UserDetailsResponseResource> GetAccountOpenedByDateRangeAsync(DateTime fromDate , DateTime toDate);
        Task<int> GetSubscribersCountByCountryAsync(int countryId);
        Task<int> GetSubscribersCountByAgeAsync(int minAge, int maxAge);
        Task<PayBillCreditPurchaseCount> GetBillPayCreditPurchaseCount();
        Task<UserNotesReponse> GetUserNotes(string userId,int messageType);
        Task<UserNotes> AddUserNotes(string userId, string addBy, string description, string note, Core.Models.MessageType messageType);
    }
}
