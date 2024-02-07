using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class CashFlowHistoryService : ICashFlowService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CashFlowHistoryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<CashFlowHistory> AddCashFlowHistoryAsync(CashFlowHistory cashFlowHistory)
        {
            return await _unitOfWork.CashFlowHistory.AddCashFlowHistoryAsync(cashFlowHistory);
        }

        public async Task<CashFlowHistory> AddCashFlowHistoryForWithdraw(CashFlowHistory cashFlowHistory)
        {
            return await _unitOfWork.CashFlowHistory.AddCashFlowHistoryForWithdraw(cashFlowHistory);
        }

        public async Task<CashFlowHistoryResponse> GetAllCashFlowHistoryAsync(int currency, int index, int size)
        {
            return await _unitOfWork.CashFlowHistory.GetAllCashFlowHistoryAsync(currency, index, size);
        }

        public async Task<CashFlowHistory> GetLatestCashFlowHistoryAsync(long walletId)
        {
            return await _unitOfWork.CashFlowHistory.GetLatestCashFlowHistoryAsync(walletId);
        }
    }
}
