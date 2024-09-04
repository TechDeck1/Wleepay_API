using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface ITransactionService
    {
        Task<TransactionResponseResource> GetTransactionsByWalletId(long walletId, int pageIndex = 0, int pageSize = 10);
        Task<IEnumerable<Transaction>> GetTransactionsByType(TransferTypes type, int pageIndex = 0, int pageSize = 10);
        Task<Transaction> GetTransactionsById(long Id);
        Task<IEnumerable<Transaction>> GetWalletsTransactions(IEnumerable<long> walletIdList);
        Task<Transaction> InitiateTransaction(Transaction transactionDetail);
        Task<Transaction> UpdateTransactionStatus(long id, Core.Enums.TransactionStatus status);
        Task<Transaction> InitiateTransactionRequest(Transaction transactionDetail);
        Task<IEnumerable<Transaction>> GetPaymentRequestByStatus(IEnumerable<long> walletIds, PaymentRequestType type, Core.Enums.TransactionStatus transferStatus, int pageSize, int pageIndex);
        Task<TransactionResponseResource> GetIncomingOutgoingPaymentRequest(IEnumerable<long> walletIds, PaymentRequestType type, Core.Enums.TransactionStatus transferStatus,int transferTypeId, int pageSize, int pageIndex);
        Task<TransactionResponseResource> GetPaymentRequestByStatusAndType(PaymentRequestType type, Core.Enums.TransactionStatus transferStatus, int transferId, int pageSize, int pageIndex);
        Task<Transaction> InitiateCashout(Transaction transactionDetail);
        Task<double> CalculateTotalTransactionAmount(long walletId, bool isMonthly);
        Task<TransactionResponseResource> GetCashoutTransactions(int pageIndex, int pageSize);
        Task<TransactionResponseResource> GetTopUpTransactions(int pageIndex, int pageSize);
        Task<TransactionResponseResource> GetTransactionsByTypeAndCurrency(int type, int currencies, int pageIndex = 0, int pageSize = 10);
        Task<TransactionResponseResource> GetTransactionsByUserIdAndType(string userId,int transferType, int pageIndex = 0, int pageSize = 10);
        Task<TransactionResponseResource> GetTransactionHistoryByUserId(string userId, int pageIndex, int pageSize);

        Task<IList<TransactionAmountBasedOnMonthsResponseModel>> GetTransactionAmountByMonth();
        Task<IList<TransactionSummaryResponseModel>> GetTransactionData();
        Task<IList<TransactionCountByTypeResponseModel>> GetTransactionCounttByTypeLD();
        Task<IList<TransactionCountByTypeResponseModel>> GetTransactionCounttByTypeUSD();


    }
}
