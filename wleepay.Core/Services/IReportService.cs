using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface IReportService
    {
        Task<UserDetailsResponseResource> GetReportOfUserByStatusAsync(int role, int status, int pageIndex = 0, int pageSize = 10);
        Task<UserDetailsResponseResource> GetInActiveSubscriberListAsync(int role);
        Task<UserDetailsResponseResource> GetActiveSubscriberListAsync(int role);
        Task<UserDetailsResponseResource> GetSubscriberAndMemberListByWalletBalanceAsync(int WalletType, int role, int balance, int walletType);
        Task<UserDetailsResponseResource> GetReportBySubscribersKYCLevelAsync(int level, int role);
        Task<UserDetailsResponseResource> GetAllActiveAccountsAsync(int role);
        Task<UserDetailsResponseResource> GetAccountOpenedByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<int> GetSubscribersCountByCountryAsync(int countryId);
        Task<TransactionResponseResource> TransactionCountSummary(int role, int transaferType, DateTime fromDate, DateTime toDate);


    }
}
