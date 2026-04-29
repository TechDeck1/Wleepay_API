using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class SochitelTransactionService : ISochitelTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SochitelTransactionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Task<SochitelTransactions> AddSochitelTransactions(SochitelTransactions sochitelTransactions)
        {
            return _unitOfWork.SochitelTransaction.AddSochitelTransaction(sochitelTransactions);
        }

        public async Task<double> GetTotalCommissionEarnedAsync(DateTime fromDate)
        {
            return await _unitOfWork.SochitelTransaction.GetTotalCommissionEarnedAsync(fromDate);
        }



    }
}
