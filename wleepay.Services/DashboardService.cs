using Stripe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
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
            var totalSubscribersWalletBalanceByLD = await _unitOfWork.UserDetail.GetTotalWalletBalanceByCurrency(WalletTypes.MemberAccount.GetHashCode(),Currencies.LRD.GetHashCode());
            var totalSubscribersWalletBalanceByUSD = await _unitOfWork.UserDetail.GetTotalWalletBalanceByCurrency(WalletTypes.MemberAccount.GetHashCode(), Currencies.USD.GetHashCode());
            var totalAgentsWalletBalanceByLD = await _unitOfWork.UserDetail.GetTotalWalletBalanceByCurrency(WalletTypes.AgentAccount.GetHashCode(), Currencies.LRD.GetHashCode());
            var totalAgentsWalletBalanceByUSD = await _unitOfWork.UserDetail.GetTotalWalletBalanceByCurrency(WalletTypes.AgentAccount.GetHashCode(), Currencies.USD.GetHashCode());
            var subscriberSignup = await _unitOfWork.UserDetail.GetSubscribersSignup(DateTime.Now.AddDays(-7));
            var subscriberSignupFor30days = await _unitOfWork.UserDetail.GetSubscribersSignup(DateTime.Now.AddDays(-30));
            var pendingKYC = await _unitOfWork.UserDetail.GetPendingKYCSubscribersByMonth();


            return new DashboardResponse
            {
                TotalSubscribers = totalSubscribers,
                ActiveSubscribers = activeSubscribers,
                ActiveLast7Days = Math.Round(activeLast7Days, 1),
                ActiveToday = Math.Round(activeToday, 2),
                ActiveLast30Days = Math.Round(activeLast30Days, 2),
                RetentionRate = Math.Round(retentionRate, 2),
                ChurnRate = Math.Round(churnRate, 2),
                InactiveLast30Days = inactiveLast30Days,
                InactivePrevious30Days = inactivePrevious30Days,
                TotalSubscribersWalletBalanceByLD = Math.Round(totalSubscribersWalletBalanceByLD, 2),
                TotalSubscribersWalletBalanceByUSD = Math.Round(totalSubscribersWalletBalanceByUSD, 2),
                TotalAgentsWalletBalanceByLD = Math.Round(totalAgentsWalletBalanceByLD, 2),
                TotalAgentsWalletBalanceByUSD = Math.Round(totalAgentsWalletBalanceByUSD, 2),
                PendingKYCSubscriber = pendingKYC.Count,
                SignUpSubscriberLast7Days = subscriberSignup.Count,
                SignUpSubscriberLast30Days = subscriberSignupFor30days.Count
                // Populate other fields accordingly
            };
        }

        public async Task<AgentSubscribersCountResponseModel> GetActiveAgentsAndSubscribersCount()
        {
            return await _unitOfWork.UserDetail.GetActiveAgentsAndSubscribersCount();
        }

        public async Task<decimal> GetEarnedCommissionAsync(string transactionType)
        {
            return await _unitOfWork.UserDetail.GetEarnedCommission(transactionType);

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

        public async Task<PayBillCreditPurchaseCount> GetBillPayCreditPurchaseCount()
        {
            return await _unitOfWork.UserDetail.GetBillPayCreditPurchaseCount();
        }
    }
}
