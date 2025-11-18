using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface ISochitelTransactionRepository
    {
        public Task<SochitelTransactions> AddSochitelTransaction(SochitelTransactions sochitelTransactions);

        Task<double> GetTotalCommissionEarnedAsync(DateTime fromDate);

    }
}
