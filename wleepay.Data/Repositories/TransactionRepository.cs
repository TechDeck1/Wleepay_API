using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Core.Services;
using wleepay.Data.Context;
using TransactionStatus = wleepay.Core.Enums.TransactionStatus;

namespace wleepay.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DbContext context) : base(context)
        {

        }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<Transaction> AddUpdateTransaction(Transaction transactionDetail)
        {
            using (IDbContextTransaction transaction = WPContext.Database.BeginTransaction())
            {
                try
                {
                    //create new transaction record or update transaction for top/payment request
                    if (transactionDetail.Id > 0)
                    {
                        var transactionToUpdate = await WPContext.Transactions.Where(t => t.Id == transactionDetail.Id).FirstOrDefaultAsync();
                        if (transactionToUpdate != null)
                        {
                            transactionToUpdate.Status = Core.Enums.TransactionStatus.Approved.GetHashCode();
                        }
                    }
                    else
                    {
                        transactionDetail.Status = Core.Enums.TransactionStatus.Approved.GetHashCode();
                        transactionDetail.CreatedDate = DateTime.Now;
                        await WPContext.Transactions.AddAsync(transactionDetail);
                    }

                    await WPContext.SaveChangesAsync();
                    //update sender wallet balance
                    var senderWallet = await WPContext.Wallet.Where(w => w.Id == transactionDetail.FromWalletId).FirstOrDefaultAsync();
                    senderWallet.Balance = (senderWallet.Balance - transactionDetail.Amount);

                    await WPContext.SaveChangesAsync();

                    //update receiver wallet balance
                    var receiverWallet = await WPContext.Wallet.Where(w => w.Id == transactionDetail.ToWalletId).FirstOrDefaultAsync();
                    receiverWallet.Balance = (receiverWallet.Balance + transactionDetail.Amount);

                    await WPContext.SaveChangesAsync();
                    //commit transaction
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    //rollback if any exception occured
                    transaction.Rollback();
                    throw ex;
                }
            }

            var newAddedTransaction = await WPContext.Transactions
                .Where(w => w.Id == transactionDetail.Id)
                .Include(w => w.FromWallet.Currency)
                .Include(w => w.TransferType)
                .Include(w => w.TransactionStatus)
                .FirstOrDefaultAsync();

            return newAddedTransaction;
        }

        public async Task<double> CalculateWalletTransaction(long walletId, bool isMonthly = false)
        {
            var transactionQuery = WPContext.Transactions.Where(t => t.FromWalletId == walletId && t.Status == TransactionStatus.Approved.GetHashCode());
            if (isMonthly)
                transactionQuery = transactionQuery.Where(t => t.CreatedDate.Month == DateTime.Today.Month);
            else
            {
                var todayStartDate = DateTime.Today.Date;
                var todayEndDate = DateTime.Today.Date.AddHours(23);
                transactionQuery = transactionQuery.Where(t => t.CreatedDate >= todayStartDate && t.CreatedDate <= todayEndDate);
            }

            var totalTransactionAmount = await transactionQuery.Select(s => s.Amount).SumAsync();
            return totalTransactionAmount;
        }

        public async Task<Transaction> CreatePaymentRequest(Transaction transactionDetail)
        {
            //create new transaction record
            transactionDetail.CreatedDate = DateTime.Now;
            transactionDetail.Status = Core.Enums.TransactionStatus.Pending.GetHashCode();

            await WPContext.Transactions.AddAsync(transactionDetail);
            await WPContext.SaveChangesAsync();

            return transactionDetail;
        }

        public async Task<IEnumerable<Transaction>> GetPaymentRequestByStatus(IEnumerable<long> walletIds, PaymentRequestType type, TransactionStatus statusType,int pageSize, int pageIndex)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var transactionQuery = WPContext.Transactions.Where(t => t.Status == statusType.GetHashCode());
            if (type == PaymentRequestType.InComing)
                transactionQuery = transactionQuery.Where(t => walletIds.Contains(t.FromWalletId) 
                || (t.TransferTypeId == TransferTypes.CashOut.GetHashCode() && walletIds.Contains(t.ToWalletId)) 
                || (t.TransferTypeId == TransferTypes.AgentCommissionCashout.GetHashCode() && walletIds.Contains(t.ToWalletId)))
                    .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                    .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                    .Include(s=>s.TransferType);
            else
                transactionQuery = transactionQuery.Where(t => walletIds.Contains(t.ToWalletId) 
                || (t.TransferTypeId == TransferTypes.CashOut.GetHashCode() && walletIds.Contains(t.FromWalletId))
                || (t.TransferTypeId == TransferTypes.AgentCommissionCashout.GetHashCode() && walletIds.Contains(t.FromWalletId)))
                      .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                      .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                      .Include(s => s.TransferType);

            return await transactionQuery.Skip(skipCount).Take(takeCount).Include(t => t.TransactionStatus).ToListAsync();
        }
   
        public async Task<Transaction> GetTransactionDetails(long transactionId)
        {
            return await WPContext.Transactions
                .Where(t => t.Id == transactionId)
                .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                .Include(s => s.FromWallet.Currency)
                .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                .Include(s => s.TransferType)
                .FirstOrDefaultAsync();
        }

        public async Task<TransactionResponseResource> GetTransactionsByWalletId(long walletId, int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var transactionQuery = WPContext.Transactions
                .Where(w => w.Status != Core.Enums.TransactionStatus.Pending.GetHashCode());
            
            if(walletId != 0)
                transactionQuery = transactionQuery.Where(w=> w.FromWalletId == walletId || w.ToWalletId == walletId);

            TransactionResponseResource response = new TransactionResponseResource();
            response.TotalCount = transactionQuery.Count();
            response.Transactions = await transactionQuery
                .OrderByDescending(w => w.CreatedDate)
                .Skip(skipCount)
                .Take(takeCount)
                .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                .Include(s => s.FromWallet.Currency)
                .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                .Include(s => s.TransferType)
                .ToListAsync();

            return response;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByType(TransferTypes type, int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var transactionQuery = WPContext.Transactions
                .Where(w => w.Status != Core.Enums.TransactionStatus.Pending.GetHashCode() && w.TransferTypeId == type.GetHashCode());

            return await transactionQuery
                .OrderByDescending(w => w.CreatedDate)
                .Skip(skipCount)
                .Take(takeCount)
                .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                .Include(s => s.FromWallet.Currency)
                .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                .Include(s => s.TransferType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetWalletsTransactions(IEnumerable<long> walletIdList)
        {
            return await WPContext.Transactions
                .Where(w => walletIdList.Contains(w.FromWalletId) || walletIdList.Contains(w.ToWalletId))
                .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                .ToListAsync();
        }

        public async Task<Transaction> UpdateTransactionStatus(long transactionId, Core.Enums.TransactionStatus status)
        {
            var transactionToUpdate = await WPContext.Transactions.Where(t => t.Id == transactionId).FirstOrDefaultAsync();
            if (transactionToUpdate != null)
            {
                transactionToUpdate.Status = status.GetHashCode();
            }

            await WPContext.SaveChangesAsync();
            return transactionToUpdate;
        }

        private bool isTodayRecord(DateTime itemDate)
        {
            var todayStartDate = DateTime.Today.Date;
            var todayEndDate = DateTime.Today.Date.AddHours(23);
            return itemDate >= todayStartDate && itemDate <= todayEndDate;
        }

        public async Task<TransactionResponseResource> GetCashoutTransactions(int pageIndex = 0, int pageSize =0 )
        {
            if (pageSize > 0)
            {
                int takeCount = pageSize;
                int skipCount = pageIndex * pageSize;
                var cashOutTransactions = await WPContext.Transactions.Where(u => u.TransferTypeId == (int)TransferTypes.CashOut)
                    .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                    .Include(s => s.FromWallet.Currency)
                    .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                    .Include(s => s.TransferType)
                    .OrderByDescending(w => w.CreatedDate)
                    .Skip(skipCount)
                    .Take(takeCount)
                    .ToListAsync();

                var TotalCount = await WPContext.Transactions.Where(u => u.TransferTypeId == (int)TransferTypes.CashOut).CountAsync();

                TransactionResponseResource response = new TransactionResponseResource();
                response.Transactions = cashOutTransactions;
                response.TotalCount = TotalCount;
                return response;
            }
            else
            {
                var cashOutTransactions = await WPContext.Transactions.Where(u => u.TransferTypeId == (int)TransferTypes.CashOut)
                   .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                   .Include(s => s.FromWallet.Currency)
                   .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                   .Include(s => s.TransferType)
                   .OrderByDescending(w => w.CreatedDate)
                   .ToListAsync();

                var TotalCount = await WPContext.Transactions.Where(u => u.TransferTypeId == (int)TransferTypes.CashOut).CountAsync();

                TransactionResponseResource response = new TransactionResponseResource();
                response.Transactions = cashOutTransactions;
                response.TotalCount = TotalCount;
                return response;
            }
        }

        public async Task<TransactionResponseResource> GetTopUpTransactions(int pageIndex = 0, int pageSize = 0)
        {
            if (pageSize > 0)
            {
                int takeCount = pageSize;
                int skipCount = pageIndex * pageSize;
                var topUpTransactions = await WPContext.Transactions.Where(u => u.TransferTypeId == (int)TransferTypes.TopUp)
                    .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                    .Include(s => s.FromWallet.Currency)
                    .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                    .Include(s => s.TransferType)
                    .OrderByDescending(w => w.CreatedDate)
                    .Skip(skipCount)
                    .Take(takeCount)
                    .ToListAsync();

                var TotalCount = await WPContext.Transactions.Where(u => u.TransferTypeId == (int)TransferTypes.TopUp).CountAsync();

                TransactionResponseResource response = new TransactionResponseResource();
                response.Transactions = topUpTransactions;
                response.TotalCount = TotalCount;
                return response;
            }
            else
            {
                var topUpTransactions = await WPContext.Transactions.Where(u => u.TransferTypeId == (int)TransferTypes.TopUp)
                  .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                  .Include(s => s.FromWallet.Currency)
                  .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                  .Include(s => s.TransferType)
                  .OrderByDescending(w => w.CreatedDate)
                  .ToListAsync();

                var TotalCount = await WPContext.Transactions.Where(u => u.TransferTypeId == (int)TransferTypes.TopUp).CountAsync();

                TransactionResponseResource response = new TransactionResponseResource();
                response.Transactions = topUpTransactions;
                response.TotalCount = TotalCount;
                return response;
            }
        }

        public  async Task<TransactionResponseResource> GetTransactionsByTypeAndCurrency(int type, int currency, int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var transactionQuery = WPContext.Transactions
                .Where(w => w.Status != Core.Enums.TransactionStatus.Pending.GetHashCode() && w.TransferTypeId == type && w.FromWallet.CurrencyId == currency);

            var result = await transactionQuery
               .OrderByDescending(w => w.CreatedDate)
               .Skip(skipCount)
               .Take(takeCount)
               .Include(s => s.FromWallet.ApplicationUser.UserDetail)
               .Include(s => s.FromWallet.Currency)
               .Include(s => s.ToWallet.ApplicationUser.UserDetail)
               .Include(s => s.TransferType)
               .ToListAsync();
            TransactionResponseResource response = new TransactionResponseResource();
            response.Transactions = result;
            response.TotalCount =await  WPContext.Transactions
                .Where(w => w.Status != Core.Enums.TransactionStatus.Pending.GetHashCode() && w.TransferTypeId == type && w.FromWallet.CurrencyId == currency).CountAsync();
            ;
            return response;

           
        }

        public async Task<TransactionResponseResource> GetTransactionsByUserIdAndType(string userId,int transferType, int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var transactionQuery = WPContext.Transactions
                .Where(w => w.Status != Core.Enums.TransactionStatus.Pending.GetHashCode() && w.TransferTypeId == transferType);

            if (!string.IsNullOrEmpty(userId))
                transactionQuery = transactionQuery.Where(w => w.ToWallet.UserId == userId || w.FromWallet.UserId == userId);

            TransactionResponseResource response = new TransactionResponseResource();
            response.TotalCount = transactionQuery.Count();
            response.Transactions = await transactionQuery
                .OrderByDescending(w => w.CreatedDate)
                .Skip(skipCount)
                .Take(takeCount)
                .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                .Include(s => s.FromWallet.Currency)
                .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                .Include(s => s.TransferType)
                .ToListAsync();

            return response;
        }
    }
}
