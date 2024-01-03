using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Enums;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactionsByWalletId(long walletId, int pageIndex = 0, int pageSize = 10);
        Task<IEnumerable<Transaction>> GetTransactionsByType(TransferTypes type, int pageIndex = 0, int pageSize = 10);
        Task<Transaction> GetTransactionsById(long Id);
        Task<IEnumerable<Transaction>> GetWalletsTransactions(IEnumerable<long> walletIdList);
        Task<Transaction> InitiateTransaction(Transaction transactionDetail);
        Task<Transaction> UpdateTransactionStatus(long id, Core.Enums.TransactionStatus status);
        Task<Transaction> InitiateTransactionRequest(Transaction transactionDetail);
        Task<IEnumerable<Transaction>> GetPaymentRequestByStatus(IEnumerable<long> walletIds, PaymentRequestType type, Core.Enums.TransactionStatus transferStatus, int pageSize, int pageIndex);
        Task<Transaction> InitiateCashout(Transaction transactionDetail);
        Task<double> CalculateTotalTransactionAmount(long walletId, bool isMonthly);
        Task<IList<Transaction>> GetCashoutTransactions(int pageIndex = 0, int pageSize = 10);
        Task<IList<Transaction>> GetTopUpTransactions(int pageIndex = 0, int pageSize = 10);


    }
}
