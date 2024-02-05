using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
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
    }
}
