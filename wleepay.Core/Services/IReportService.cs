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
        Task<int> GetSubscribersCountByAgeAsync(int minAge,int maxAge);
        Task<TransactionResponseResource> TransactionCountSummary(int role, int transaferType, DateTime fromDate, DateTime toDate);
        Task<SochitelTransactionResponseResource> SochitelCommisionTransactionSummary(int role, string transactionType, DateTime fromDate, DateTime toDate);
        Task<WalletsBeginingBalance> getStartingWalletBalance(DateTime reportStartDate, int walletType, int currencyId);
        Task<WalletsEndingBalance> getEndingWalletBalance(DateTime reportStartDate, int walletType, int currencyId);
        Task<CreditsByWleepay> getCreditsForAgentAndMemberByWleepay();
        Task<DebitsByWleepay> getDebitsForAgentAndMemberByWleepay();

        CashOnHandReponse getCashOnHandByWleepay(DateTime reportStartDate, DateTime reportEndDate, int walletType, int currencyType,int PageSize, int PageIndex);

        GetBillPaymentCommissionEarnedResponse getBillPaymentCommissionEarnedReport(DateTime reportStartDate, DateTime reportEndDate, string type, int pageIndex, int pageSize, int currencyId);
        GetCashOutCommissionEarnedResponse getcashOutCommissionEarnedReport(DateTime reportStartDate, DateTime reportEndDate, string type, int pageIndex,int pageSize,int currencyId);

        GetFloatAccountResponse getFloatAccountReport(DateTime reportStartDate, DateTime reportEndDate, int walletType, int pageIndex, int pageSize, int currencyId);

        GetReconciliationResponse getReconciliationReport(int pageIndex, int pageSize, int currencyId, DateTime reportStartDate,string userId);

        List<GetReconciliationMonthlyBalance> getReconciliationReportByMonth(int pageIndex, int pageSize, int currencyId, DateTime reportStartDate, string userId);

    }
}
