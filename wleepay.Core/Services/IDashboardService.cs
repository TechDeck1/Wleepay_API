using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;

namespace wleepay.Core.Services
{
    public interface IDashboardService
    {
        public Task<DashboardResponse> GetDashboardDataAsync();
        Task<AgentSubscribersCountResponseModel> GetActiveAgentsAndSubscribersCount();
        Task<PayBillCreditPurchaseCount> GetBillPayCreditPurchaseCount();
        Task<IList<CountByMonths>> GetActiveAgentsByMonth();
        Task<decimal> GetEarnedCommissionAsync(string transactionType);
        Task<IList<CountByMonths>> GetActiveSubscribersByMonth();
        Task<IList<subscribersByType>> GetActiveSubscribersByType();
        Task<IList<TransactionAmountBasedOnMonthsResponseModel>> GetTransactionAmountByMonthLD();
        Task<IList<TransactionAmountBasedOnMonthsResponseModel>> GetTransactionAmountByMonthUSD();
        Task<IList<TransactionSummaryResponseModel>> GetTransactionData(int monthId, int currencyId);
        Task<IList<TransactionCountByTypeResponseModel>> GetTransactionCounttByTypeLD();
        Task<IList<TransactionCountByTypeResponseModel>> GetTransactionCounttByTypeUSD();
        Task<IList<WalletAmountHistoryByMonths>> GetWalletHistoryByWalletTypeAndCurrency(int walletType, int currencyId);

    }
}
