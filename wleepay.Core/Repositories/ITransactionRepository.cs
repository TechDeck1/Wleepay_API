using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetTransactionsByWalletId(long walletId, int pageIndex = 0, int pageSize = 10);
        Task<IEnumerable<Transaction>> GetTransactionsByType(Core.Enums.TransferTypes type, int pageIndex = 0, int pageSize = 10);
        Task<IEnumerable<Transaction>> GetWalletsTransactions(IEnumerable<long> walletIdList);
        Task<Transaction> AddUpdateTransaction(Transaction transactionDetail);
        Task<Transaction> UpdateTransactionStatus(long transactionId, Core.Enums.TransactionStatus status);
   
        Task<IEnumerable<Transaction>> GetPaymentRequestByStatus(IEnumerable<long> walletIds, Core.Enums.PaymentRequestType type, Core.Enums.TransactionStatus transferStatus, int pageSize, int pageIndex);
        Task<Transaction> CreatePaymentRequest(Transaction transactionDetail);
        Task<Transaction> GetTransactionDetails(long transactionId);
        Task<double> CalculateWalletTransaction(long walletId, bool isMonthly = false);
        Task<TransactionResponseResource> GetCashoutTransactions(int pageIndex = 0, int pageSize = 0);
        Task<TransactionResponseResource> GetTopUpTransactions(int pageIndex = 0, int pageSize= 0);


    }
}
