using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<TransactionResponseResource> GetTransactionsByWalletId(long walletId, int pageIndex = 0, int pageSize = 10);
        Task<IEnumerable<Transaction>> GetTransactionsByType(Core.Enums.TransferTypes type, int pageIndex = 0, int pageSize = 10);
        Task<IEnumerable<Transaction>> GetWalletsTransactions(IEnumerable<long> walletIdList);
        Task<Transaction> AddUpdateTransaction(Transaction transactionDetail);
        Task<Transaction> UpdateTransactionStatus(long transactionId, Core.Enums.TransactionStatus status);
        Task<IEnumerable<Transaction>> GetPaymentRequestByStatus(IEnumerable<long> walletIds, Core.Enums.PaymentRequestType type, Core.Enums.TransactionStatus transferStatus, int pageSize, int pageIndex);
        Task<TransactionResponseResource> GetIncomingOutgoingPaymentRequest(IEnumerable<long> walletIds, Core.Enums.PaymentRequestType type, Core.Enums.TransactionStatus transferStatus,int transferTypeId,  int pageSize, int pageIndex);
        Task<TransactionResponseResource> GetPaymentRequestByStatusAndType(Core.Enums.PaymentRequestType type, Core.Enums.TransactionStatus transferStatus, int transferId, int pageSize, int pageIndex);
        Task<Transaction> CreatePaymentRequest(Transaction transactionDetail);
        Task<Transaction> GetTransactionDetails(long transactionId);
        Task<double> CalculateWalletTransaction(long walletId, bool isMonthly = false);
        Task<TransactionResponseResource> GetCashoutTransactions(int pageIndex = 0, int pageSize = 0);
        Task<TransactionResponseResource> GetTopUpTransactions(int pageIndex = 0, int pageSize= 0);
        Task<TransactionResponseResource> GetTransactionsByTypeAndCurrency(int type, int currency, int pageIndex = 0, int pageSize = 10);
        Task<TransactionResponseResource> GetTransactionsByUserIdAndType(string userId,int transeferType, int pageIndex = 0, int pageSize = 10);
        Task<TransactionResponseResource> GetTransactionsHistoryByUserId(string userId, int pageIndex = 0, int pageSize = 10);
        Task<IList<TransactionAmountBasedOnMonthsResponseModel>> GetTransactionAmountByMonth();
        Task<IList<TransactionCountByTypeResponseModel>> GetTransactionCounttByTypeLD();
        Task<IList<TransactionCountByTypeResponseModel>> GetTransactionCounttByTypeUSD();



    }
}
