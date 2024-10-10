using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.DTO.Response;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class DashboardService:IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DashboardService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<DashboardResponse> GetDashboardDataAsync()
        {
            var totalSubscribers = await _unitOfWork.UserDetail.GetTotalSubscribersAsync();
            var activeSubscribers = await _unitOfWork.UserDetail.GetActiveSubscribersAsync();
            var retentionRate = await _unitOfWork.UserDetail.GetActiveSubscriberLast30DaysAsync();
            var activeLast30Days = await _unitOfWork.UserDetail.GetActiveSubscriberLast30DaysAsync();
            var activeLast7Days = await _unitOfWork.UserDetail.GetActiveSubscriberLast7daysAsync();
            var activeToday = await _unitOfWork.UserDetail.GetActiveSubscriberTodayAsync();
            var churnRate = await _unitOfWork.UserDetail.GetSubscriberChurnRateAsync();
            var inactiveLast30Days = await _unitOfWork.UserDetail.GetInactiveSubscribersAsync(DateTime.Now.AddDays(-30));
            var inactivePrevious30Days = await _unitOfWork.UserDetail.GetInactiveSubscribersAsync(DateTime.Now.AddDays(-60));

            return new DashboardResponse
            {
                TotalSubscribers = totalSubscribers,
                ActiveSubscribers = activeSubscribers,
                activeLast7Days = activeLast7Days,
                activeToday = activeToday,
                activeLast30Days = activeLast30Days,
                RetentionRate = retentionRate,
                ChurnRate = churnRate,
                InactiveLast30Days = inactiveLast30Days,
                InactivePrevious30Days = inactivePrevious30Days
                // Populate other fields accordingly
            };
        }

        public async Task<AgentSubscribersCountResponseModel> GetActiveAgentsAndSubscribersCount()
        {
            return await _unitOfWork.UserDetail.GetActiveAgentsAndSubscribersCount();

        }
        public async Task<IList<CountByMonths>> GetActiveAgentsByMonth()
        {
            return await _unitOfWork.UserDetail.GetActiveAgentsByMonth();
        }

        public async Task<IList<CountByMonths>> GetActiveSubscribersByMonth()
        {
            return await _unitOfWork.UserDetail.GetActiveSubscribersByMonth();
        }
        public async Task<IList<subscribersByType>> GetActiveSubscribersByType()
        {
            return await _unitOfWork.UserDetail.GetActiveSubscribersByType();
        }

        public async Task<IList<TransactionAmountBasedOnMonthsResponseModel>> GetTransactionAmountByMonthLD()
        {
            return await _unitOfWork.Transaction.GetTransactionAmountByMonthLD();

        }

        public async Task<IList<TransactionAmountBasedOnMonthsResponseModel>> GetTransactionAmountByMonthUSD()
        {
            return await _unitOfWork.Transaction.GetTransactionAmountByMonthUSD();

        }

        public async Task<IList<TransactionCountByTypeResponseModel>> GetTransactionCounttByTypeLD()
        {
            return await _unitOfWork.Transaction.GetTransactionCounttByTypeLD();

        }

        public async Task<IList<TransactionCountByTypeResponseModel>> GetTransactionCounttByTypeUSD()
        {
            return await _unitOfWork.Transaction.GetTransactionCounttByTypeUSD();

        }

        public async Task<IList<TransactionSummaryResponseModel>> GetTransactionData(int monthId, int currencyId)
        {
            return await _unitOfWork.Transaction.GetTransactionData(monthId, currencyId);

        }
        public async Task<IList<WalletAmountHistoryByMonths>> GetWalletHistoryByWalletTypeAndCurrency(int walletType, int currencyId)
        {
            return await _unitOfWork.Wallet.GetWalletHistoryByWalletTypeAndCurrency(walletType, currencyId);
        }
    }
}
