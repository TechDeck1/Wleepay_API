using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Request;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface IPaymentTransactionService
    {
        Task SaveTransactionAsync(PaymentTransactionDto transactionDto);
        Task<PaymentTransaction> GetByTransactionIdAsync(string transactionId);

        Task<int> GetTransactionCountAsync();
        Task<decimal> GetTotalTransactionVolumeAsync();

        Task<PaginatedTransactionResult<TransactionHistory>> GetTransactionsByUserIdAsync(string userId, int? transferTypeId, int pageNumber, int pageSize);
       
        Task<PaginatedTransactionResult<TransactionReport>> GetTransactionsByTypeAndDateRange(int transferTypeId, DateTime startDate, DateTime endDate, int pageNumber, int pageSize);

    }


}
