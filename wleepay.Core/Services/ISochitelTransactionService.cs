using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;


namespace wleepay.Core.Services
{
    public interface ISochitelTransactionService
    {
        public Task<SochitelTransactions> AddSochitelTransactions(SochitelTransactions sochitelTransactions);


        //public Task SaveAirtimeRechargeTransactionAsync(SochitelTransactionResponse response, PaymentRequests request, string userReference, int? cardTransactionId);
        Task<double> GetTotalCommissionEarnedAsync(DateTime fromDate);

    }
}
