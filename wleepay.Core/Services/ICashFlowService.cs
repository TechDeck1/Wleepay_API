using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface ICashFlowService
    {
        Task<CashFlowHistory> AddCashFlowHistoryAsync(CashFlowHistory cashFlowHistory);
        Task<CashFlowHistory> AddCashFlowHistoryForWithdraw(CashFlowHistory cashFlowHistory);
        Task<CashFlowHistory> GetLatestCashFlowHistoryAsync(long WalletId);
        Task<IEnumerable<CashFlowHistory>> GetAllCashFlowHistoryAsync();

    }
}
