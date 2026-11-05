using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface IPaymentTransactionRepository
    {
        Task SaveTransactionAsync(PaymentTransaction transaction);
        Task<PaymentTransaction> GetByTransactionIdAsync(string transactionId);
        Task<int> GetTransactionCountAsync();
        Task<decimal> GetTotalTransactionVolumeAsync();
        Task<PaginatedTransactionResult<TransactionHistory>> GetTransactionsByUserIdAsync(string userId, int? transferTypeId, int pageNumber, int pageSize);
        Task<PaginatedTransactionResult<TransactionReport>> GetTransactionsByTypeAndDateRange(int transferTypeId, DateTime startDate, DateTime endDate, int pageNumber, int pageSize);
        Task<MobileTransactionPage> GetMobileTransactionsAsync(string userId, string? kind, int pageNumber, int pageSize);


    }
}
