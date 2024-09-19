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
        Task<IEnumerable<Transaction>> GetIncomingOutGoingPaymentRequest(IEnumerable<long> walletIds, Core.Enums.PaymentRequestType type, Core.Enums.TransactionStatus transferStatus, int pageSize, int pageIndex);
        Task<TransactionResponseResource> GetIncomingOutGoingPaymentRequestByTransaferTypeId(IEnumerable<long> walletIds, Core.Enums.PaymentRequestType type, Core.Enums.TransactionStatus transferStatus,int transferTypeId,  int pageSize, int pageIndex);
        Task<TransactionResponseResource> GetPaymentRequestByStatusAndType(Core.Enums.PaymentRequestType type, Core.Enums.TransactionStatus transferStatus, int transferId, int pageSize, int pageIndex);
        Task<Transaction> CreatePaymentRequest(Transaction transactionDetail);
        Task<Transaction> GetTransactionDetails(long transactionId);
        Task<double> CalculateWalletTransaction(long walletId, bool isMonthly = false);
        Task<TransactionResponseResource> GetTransactionsByTypeAndCurrency(int type, int currency, int pageIndex = 0, int pageSize = 10);
        Task<TransactionResponseResource> GetTransactionsByUserIdAndType(string userId,int transeferType, int pageIndex = 0, int pageSize = 10);
        Task<TransactionResponseResource> GetTransactionsHistoryByUserIdAndType(string userId,int status, int transeferType, int pageIndex = 0, int pageSize = 10);
        Task<TransactionResponseResource> GetTransactionsHistoryByUserId(string userId, int pageIndex = 0, int pageSize = 10);
        Task<IList<TransactionAmountBasedOnMonthsResponseModel>> GetTransactionAmountByMonthLD();
        Task<IList<TransactionAmountBasedOnMonthsResponseModel>> GetTransactionAmountByMonthUSD();
        Task<IList<TransactionCountByTypeResponseModel>> GetTransactionCounttByTypeLD();
        Task<IList<TransactionCountByTypeResponseModel>> GetTransactionCounttByTypeUSD();
        Task<IList<TransactionSummaryResponseModel>> GetTransactionData(int monthId, int currencyId);



    }
}
