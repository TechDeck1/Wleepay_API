using DevExpress.Data.Linq.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Management.Smo;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Common;
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


        public async void AddTransaction(Transaction transaction)
        {
            await WPContext.Transactions.AddAsync(transaction);
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

        public async Task<Transaction> AddUpdateMemberCashoutTransactions(WalletTransactions transactionDetail)
        {
            using (IDbContextTransaction transaction = WPContext.Database.BeginTransaction())
            {
                try
                {
                    //create new transaction record or update transaction for top/payment request
                    if (transactionDetail.AgentCashout.Id > 0)
                    {
                        var transactionToUpdate = await WPContext.Transactions.Where(t => t.Id == transactionDetail.AgentCashout.Id).FirstOrDefaultAsync();
                        if (transactionToUpdate != null)
                        {
                            transactionToUpdate.Status = Core.Enums.TransactionStatus.Approved.GetHashCode();
                        }
                    }
                    else
                    {
                        transactionDetail.AgentCashout.Status = Core.Enums.TransactionStatus.Approved.GetHashCode();
                        transactionDetail.AgentCashout.CreatedDate = DateTime.Now;
                        await WPContext.Transactions.AddAsync(transactionDetail.AgentCashout);
                    }

                    await WPContext.SaveChangesAsync();

                    var senderWallet = new Wallet();
                    var receiverWallet = new Wallet();


                    if (transactionDetail.commissionTransaction.TransferTypeId == TransferTypes.GeneratedAgentCommission.GetHashCode()) 
                    {
                        
                        transactionDetail.commissionTransaction.ParentId = transactionDetail.commissionTransaction.Id;
                        await WPContext.Transactions.AddAsync(transactionDetail.commissionTransaction);
                        await WPContext.SaveChangesAsync();

                        senderWallet = await WPContext.Wallet.Where(w => w.Id == transactionDetail.commissionTransaction.FromWalletId).FirstOrDefaultAsync();
                        senderWallet.Balance = (senderWallet.Balance - transactionDetail.commissionTransaction.Amount);

                        await WPContext.SaveChangesAsync();

                        //update receiver wallet balance
                        receiverWallet = await WPContext.Wallet.Where(w => w.Id == transactionDetail.commissionTransaction.ToWalletId).FirstOrDefaultAsync();
                        receiverWallet.Balance = (receiverWallet.Balance + transactionDetail.commissionTransaction.Amount);

                        await WPContext.SaveChangesAsync();
                    }

                    if (transactionDetail.taxAmount.TransferTypeId == TransferTypes.WleeTax.GetHashCode())
                    {
                        transactionDetail.taxAmount.ParentId = transactionDetail.AgentCashout.Id;
                        await WPContext.Transactions.AddAsync(transactionDetail.taxAmount);
                        await WPContext.SaveChangesAsync();

                        senderWallet = await WPContext.Wallet.Where(w => w.Id == transactionDetail.taxAmount.FromWalletId).FirstOrDefaultAsync();
                        senderWallet.Balance = (senderWallet.Balance - transactionDetail.taxAmount.Amount);

                        await WPContext.SaveChangesAsync();

                        //update receiver wallet balance
                        receiverWallet = await WPContext.Wallet.Where(w => w.Id == transactionDetail.taxAmount.ToWalletId).FirstOrDefaultAsync();
                        receiverWallet.Balance = (receiverWallet.Balance + transactionDetail.taxAmount.Amount);

                        await WPContext.SaveChangesAsync();
                    }

                    if(transactionDetail.organizationFeeAmount.TransferTypeId == TransferTypes.CashOutStandardFeeSplit.GetHashCode())
                    {
                        transactionDetail.organizationFeeAmount.ParentId = transactionDetail.AgentCashout.Id;
                        await WPContext.Transactions.AddAsync(transactionDetail.organizationFeeAmount);
                        await WPContext.SaveChangesAsync();

                        senderWallet = await WPContext.Wallet.Where(w => w.Id == transactionDetail.organizationFeeAmount.FromWalletId).FirstOrDefaultAsync();
                        senderWallet.Balance = (senderWallet.Balance - transactionDetail.organizationFeeAmount.Amount);

                        await WPContext.SaveChangesAsync();

                        //update receiver wallet balance
                        receiverWallet = await WPContext.Wallet.Where(w => w.Id == transactionDetail.organizationFeeAmount.ToWalletId).FirstOrDefaultAsync();
                        receiverWallet.Balance = (receiverWallet.Balance + transactionDetail.organizationFeeAmount.Amount);

                        await WPContext.SaveChangesAsync();

                    }

                    if (transactionDetail.AgentCashout.TransferTypeId == TransferTypes.CashOut.GetHashCode())
                    {

                        //update sender wallet balance
                        senderWallet = await WPContext.Wallet.Where(w => w.Id == transactionDetail.AgentCashout.FromWalletId).FirstOrDefaultAsync();
                        senderWallet.Balance = (senderWallet.Balance - transactionDetail.AgentCashout.Amount);

                        await WPContext.SaveChangesAsync();

                        //update receiver wallet balance
                        receiverWallet = await WPContext.Wallet.Where(w => w.Id == transactionDetail.AgentCashout.ToWalletId).FirstOrDefaultAsync();
                        receiverWallet.Balance = (receiverWallet.Balance + transactionDetail.AgentCashout.Amount);

                        await WPContext.SaveChangesAsync();
                    }


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
                .Where(w => w.Id == transactionDetail.AgentCashout.Id)
                .Include(w => w.FromWallet.Currency)
                .Include(w => w.TransferType)
                .Include(w => w.TransactionStatus)
                .FirstOrDefaultAsync();

            return newAddedTransaction;
        }

        public async Task<Transaction> AddUpdateAgentAndMemberTopUpTransaction(Transaction transactionDetail)
        {
            using (IDbContextTransaction transaction = WPContext.Database.BeginTransaction())
            {
                try
                {
                    var cashOnHandWallet = new Wallet();
                    //create new transaction record or update transaction for top/payment request
                    if (transactionDetail.Id > 0)
                    {
                        var transactionToUpdate = await WPContext.Transactions.Where(t => t.Id == transactionDetail.Id).FirstOrDefaultAsync();
                        if (transactionToUpdate != null)
                        {
                            transactionToUpdate.Status = Core.Enums.TransactionStatus.Approved.GetHashCode();
                            await WPContext.SaveChangesAsync();

                            var wallet = await WPContext.Wallet.Where(x=> x.Id == transactionToUpdate.FromWalletId && x.Type == WalletTypes.SystemWallet.GetHashCode()).FirstOrDefaultAsync();

                            cashOnHandWallet = await WPContext.Wallet.Where(x => x.UserId == wallet.UserId && x.CurrencyId == wallet.CurrencyId && x.Type == WalletTypes.DebitFloatN.GetHashCode()).FirstOrDefaultAsync();

                            Transaction addNewTransaction = new Transaction();
                            addNewTransaction.FromWalletId = null;
                            addNewTransaction.ToWalletId = cashOnHandWallet.Id;
                            addNewTransaction.Status = TransactionStatus.Approved.GetHashCode();
                            addNewTransaction.TransferTypeId = TransferTypes.InHandCredit.GetHashCode();
                            addNewTransaction.CreatedDate = DateTime.Now;
                            addNewTransaction.ParentId = transactionToUpdate.Id;
                            addNewTransaction.Description = "Credited to cash on hand wallet";
                            addNewTransaction.Amount = transactionToUpdate.Amount;

                            await WPContext.Transactions.AddAsync(addNewTransaction);
                            await WPContext.SaveChangesAsync();

                        }
                    }
                    else
                    {
                        transactionDetail.Status = Core.Enums.TransactionStatus.Approved.GetHashCode();
                        transactionDetail.CreatedDate = DateTime.Now;
                        var addedTransaction = await WPContext.Transactions.AddAsync(transactionDetail);

                        var wallet = await WPContext.Wallet.Where(x => x.Id == transactionDetail.FromWalletId && x.Type == WalletTypes.SystemWallet.GetHashCode()).FirstOrDefaultAsync();

                        cashOnHandWallet = await WPContext.Wallet.Where(x => x.UserId == wallet.UserId && x.CurrencyId == wallet.CurrencyId && x.Type == WalletTypes.DebitFloatN.GetHashCode()).FirstOrDefaultAsync();

                        Transaction addNewTransaction = new Transaction();
                        addNewTransaction.FromWalletId = null;
                        addNewTransaction.ToWalletId = cashOnHandWallet.Id;
                        addNewTransaction.Status = TransactionStatus.Approved.GetHashCode();
                        addNewTransaction.TransferTypeId = TransferTypes.InHandCredit.GetHashCode();
                        addNewTransaction.CreatedDate = DateTime.Now;
                        addNewTransaction.ParentId = transactionDetail.Id;
                        addNewTransaction.Description = "Credited to cash on hand wallet";
                        addNewTransaction.Amount = transactionDetail.Amount;

                        await WPContext.Transactions.AddAsync(addNewTransaction);
                        await WPContext.SaveChangesAsync();
                    }
                    await WPContext.SaveChangesAsync();

                    //if (transactionDetail.TransferTypeId == TransferTypes.FillAgent.GetHashCode() || transactionDetail.TransferTypeId == TransferTypes.MemberTopUpByWleePay.GetHashCode()) 
                    //{
                        //update sender wallet balance
                        var senderWallet = await WPContext.Wallet.Where(w => w.Id == transactionDetail.FromWalletId).FirstOrDefaultAsync();
                        senderWallet.Balance = (senderWallet.Balance - transactionDetail.Amount);

                        await WPContext.SaveChangesAsync();

                        //update receiver wallet balance
                        var receiverWallet = await WPContext.Wallet.Where(w => w.Id == transactionDetail.ToWalletId).FirstOrDefaultAsync();
                        receiverWallet.Balance = (receiverWallet.Balance + transactionDetail.Amount);

                        await WPContext.SaveChangesAsync();
                    
                        cashOnHandWallet.Balance = (cashOnHandWallet.Balance + transactionDetail.Amount);
                       
                        await WPContext.SaveChangesAsync();

                    //}

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

        public async Task<Transaction> AddUpdateAgentCashOutTransactions(Transaction transactionDetail)
        {
            using (IDbContextTransaction transaction = WPContext.Database.BeginTransaction())
            {
                try
                {
                    var cashOnHandWallet = new Wallet();
                    //create new transaction record or update transaction for top/payment request
                    if (transactionDetail.Id > 0)
                    {

                        var transactionToUpdate = await WPContext.Transactions.Where(t => t.Id == transactionDetail.Id).FirstOrDefaultAsync();
                        if (transactionToUpdate != null)
                        {
                            transactionToUpdate.Status = Core.Enums.TransactionStatus.Approved.GetHashCode();
                            await WPContext.SaveChangesAsync();

                            var wallet = await WPContext.Wallet.Where(x => x.Id == transactionToUpdate.ToWalletId && x.Type == WalletTypes.SystemWallet.GetHashCode()).FirstOrDefaultAsync();

                            cashOnHandWallet = await WPContext.Wallet.Where(x => x.UserId == wallet.UserId && x.CurrencyId == wallet.CurrencyId && x.Type == WalletTypes.DebitFloatN.GetHashCode()).FirstOrDefaultAsync();

                            Transaction addNewTransaction = new Transaction();
                            addNewTransaction.FromWalletId = cashOnHandWallet.Id;
                            addNewTransaction.ToWalletId = null;
                            addNewTransaction.Status = TransactionStatus.Approved.GetHashCode();
                            addNewTransaction.TransferTypeId = TransferTypes.InHandCredit.GetHashCode();
                            addNewTransaction.CreatedDate = DateTime.Now;
                            addNewTransaction.ParentId = transactionToUpdate.Id;
                            addNewTransaction.Description = "Debited from cash on hand wallet";
                            addNewTransaction.Amount = transactionToUpdate.Amount;

                            await WPContext.Transactions.AddAsync(addNewTransaction);
                            await WPContext.SaveChangesAsync();

                        }
                    }
                    else
                    {
                        transactionDetail.Status = Core.Enums.TransactionStatus.Approved.GetHashCode();
                        transactionDetail.CreatedDate = DateTime.Now;
                        await WPContext.Transactions.AddAsync(transactionDetail);
                        await WPContext.SaveChangesAsync();

                        var wallet = await WPContext.Wallet.Where(x => x.Id == transactionDetail.FromWalletId && x.Type == WalletTypes.SystemWallet.GetHashCode()).FirstOrDefaultAsync();
                        cashOnHandWallet = await WPContext.Wallet.Where(x => x.UserId == wallet.UserId && x.CurrencyId == wallet.CurrencyId && x.Type == WalletTypes.DebitFloatN.GetHashCode()).FirstOrDefaultAsync();

                        Transaction addNewTransaction = new Transaction();
                        addNewTransaction.FromWalletId = cashOnHandWallet.Id;
                        addNewTransaction.ToWalletId = null;
                        addNewTransaction.Status = TransactionStatus.Approved.GetHashCode();
                        addNewTransaction.TransferTypeId = TransferTypes.InHandWithdrawl.GetHashCode();
                        addNewTransaction.CreatedDate = DateTime.Now;
                        addNewTransaction.ParentId = transactionDetail.Id;
                        addNewTransaction.Description = "Debited from cash on hand wallet";
                        addNewTransaction.Amount = transactionDetail.Amount;

                        await WPContext.Transactions.AddAsync(addNewTransaction);
                        await WPContext.SaveChangesAsync();
                    }

                        //update sender wallet balance
                        var senderWallet = await WPContext.Wallet.Where(w => w.Id == transactionDetail.FromWalletId).FirstOrDefaultAsync();
                        senderWallet.Balance = (senderWallet.Balance - transactionDetail.Amount);

                        await WPContext.SaveChangesAsync();

                        //update receiver wallet balance
                        var receiverWallet = await WPContext.Wallet.Where(w => w.Id == transactionDetail.ToWalletId).FirstOrDefaultAsync();
                        receiverWallet.Balance = (receiverWallet.Balance + transactionDetail.Amount);

                        await WPContext.SaveChangesAsync();

                        cashOnHandWallet.Balance = (cashOnHandWallet.Balance - transactionDetail.Amount);

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

        public async Task<IEnumerable<Transaction>> GetPaymentRequestByStatus(IEnumerable<long> walletIds, PaymentRequestType type, TransactionStatus statusType, int pageSize, int pageIndex)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var transactionQuery = WPContext.Transactions.Where(t => t.Status == statusType.GetHashCode());

            if (type == PaymentRequestType.InComing)
                transactionQuery = transactionQuery.Where(t => walletIds.Contains((long)t.FromWalletId)
                || (t.TransferTypeId == TransferTypes.CashOut.GetHashCode() && walletIds.Contains((long)t.ToWalletId))
                || (t.TransferTypeId == TransferTypes.AgentCommissionCashout.GetHashCode() && walletIds.Contains((long)t.ToWalletId))
                || (t.TransferTypeId == TransferTypes.MemberCashOutByWleePay.GetHashCode() && walletIds.Contains((long)t.ToWalletId))
                || (t.TransferTypeId == TransferTypes.MemberTopUpByWleePay.GetHashCode() && walletIds.Contains((long)t.ToWalletId))
                || (t.TransferTypeId == TransferTypes.TopUp.GetHashCode() && walletIds.Contains((long)t.ToWalletId)))
                    .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                    .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                    .Include(s => s.TransferType);

            else
                transactionQuery = transactionQuery.Where(t => walletIds.Contains((long)t.ToWalletId)
                || (t.TransferTypeId == TransferTypes.CashOut.GetHashCode() && walletIds.Contains((long)t.FromWalletId))
                || (t.TransferTypeId == TransferTypes.TopUp.GetHashCode() && walletIds.Contains((long)t.FromWalletId))
                || (t.TransferTypeId == TransferTypes.AgentCommissionCashout.GetHashCode() && walletIds.Contains((long)t.FromWalletId)))
                      .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                      .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                      .Include(s => s.TransferType);

            return await transactionQuery.Where(x=>x.FromWallet != null && x.ToWallet != null).Skip(skipCount).Take(takeCount).Include(t => t.TransactionStatus).ToListAsync();
        }

        public async Task<TransactionResponseResource> GetPaymentRequestByStatusAndType(PaymentRequestType type, TransactionStatus statusType,int transferId, int pageSize, int pageIndex)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var transactionQuery = WPContext.Transactions.Where(t => t.Status == statusType.GetHashCode());

            transactionQuery = transactionQuery.Where(t => t.TransferTypeId == transferId)
                        .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                        .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                        .Include(s=> s.ToWallet.Currency)
                        .Include(s => s.TransferType);

            
            return new TransactionResponseResource
            {
                TotalCount = await transactionQuery.CountAsync(),
                Transactions = await transactionQuery.Skip(skipCount).Take(takeCount).Include(t => t.TransactionStatus).ToListAsync()
            };

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

            if (walletId != 0)
                transactionQuery = transactionQuery.Where(w => w.FromWalletId == walletId || w.ToWalletId == walletId);

            TransactionResponseResource response = new TransactionResponseResource();
            response.TotalCount = transactionQuery.Count();
            response.Transactions = await transactionQuery
                .OrderByDescending(w => w.CreatedDate)
                .Skip(skipCount)
                .Take(takeCount)
                .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                .Include(s => s.FromWallet.Currency)
                .Include(s => s.ToWallet.Currency)
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
                .Where(w => walletIdList.Contains((long)w.FromWalletId) || walletIdList.Contains((long)w.ToWalletId))
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

        public async Task<IEnumerable<Transaction>> UpdateTransactionStatusInBulk(IEnumerable<long> transactionIds, Core.Enums.TransactionStatus status)
        {
            var transactionToUpdate = await WPContext.Transactions.Where(t => transactionIds.Contains(t.Id)).ToListAsync();
            if (transactionToUpdate != null)
            {
                foreach (var transaction in transactionToUpdate)
                {
                    transaction.Status = status.GetHashCode();
                }
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

        public  async Task<TransactionResponseResource> GetTransactionsByTypeAndCurrency(int type, int currency, int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var transactionQuery = WPContext.Transactions
                .Where(w => w.Status != Core.Enums.TransactionStatus.Pending.GetHashCode() && w.TransferTypeId == type && (w.FromWallet.CurrencyId == currency || w.ToWallet.CurrencyId == currency));

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

        public async Task<TransactionResponseResource> GetTransactionsHistoryByUserIdAndType(string userId, int status, int transferType, int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;

            IQueryable<Transaction> transactionQuery;
            if (status == 0 && transferType == 0) 
            {
                transactionQuery = WPContext.Transactions;
            }
            else 
            {
                transactionQuery = WPContext.Transactions
                    .Where(w => w.TransferTypeId == transferType);
            }

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

        public async Task<TransactionResponseResource> GetTransactionsHistoryByUserId(string userId, int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var transactionQuery = WPContext.Transactions
                .Where(w => w.Status != Core.Enums.TransactionStatus.Pending.GetHashCode());

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

        public async Task<TransactionResponseResource> GetIncomingOutgoingPaymentRequest(IEnumerable<long> walletIds, PaymentRequestType type, TransactionStatus statusType, int transferTypeId, int pageSize, int pageIndex)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var transactionQuery = WPContext.Transactions.Where(t => t.Status == statusType.GetHashCode());

            if(transferTypeId == TransferTypes.TopUp.GetHashCode())
            {
                transactionQuery = transactionQuery.Where(t => walletIds.Contains((long)t.ToWalletId)
                && (t.TransferTypeId == transferTypeId && walletIds.Contains((long)t.ToWalletId)))
                    .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                    .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                    .Include(s => s.TransferType);
            }
            if (transferTypeId == TransferTypes.CashOut.GetHashCode())
            {
                transactionQuery = transactionQuery.Where(t => walletIds.Contains((long)t.FromWalletId)
                && (t.TransferTypeId == transferTypeId && walletIds.Contains((long)t.FromWalletId)))
                    .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                    .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                    .Include(s => s.TransferType);
            }
            if (transferTypeId == TransferTypes.FillAgent.GetHashCode())
            {
                transactionQuery = transactionQuery.Where(t => walletIds.Contains((long)t.ToWalletId)
                && (t.TransferTypeId == transferTypeId && walletIds.Contains((long)t.ToWalletId)))
                    .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                    .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                    .Include(s => s.TransferType);
            }
            if (transferTypeId == TransferTypes.AgentCommissionCashout.GetHashCode())
            {
                transactionQuery = transactionQuery.Where(t => walletIds.Contains((long)t.FromWalletId)
                && (t.TransferTypeId == transferTypeId && walletIds.Contains((long)t.FromWalletId)))
                    .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                    .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                    .Include(s => s.TransferType);
            }
            if (transferTypeId == TransferTypes.AgentCashout.GetHashCode())
            {
                transactionQuery = transactionQuery.Where(t => walletIds.Contains((long)t.FromWalletId)
                && (t.TransferTypeId == transferTypeId && walletIds.Contains((long)t.FromWalletId)))
                    .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                    .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                    .Include(s => s.TransferType);
            }
            if (transferTypeId == TransferTypes.MemberCashOutByWleePay.GetHashCode())
            {
                transactionQuery = transactionQuery.Where(t => walletIds.Contains((long)t.FromWalletId)
                && (t.TransferTypeId == transferTypeId && walletIds.Contains((long)t.FromWalletId)))
                    .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                    .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                    .Include(s => s.TransferType);
            }
            if (transferTypeId == TransferTypes.MemberTopUpByWleePay.GetHashCode())
            {
                transactionQuery = transactionQuery.Where(t => walletIds.Contains((long)t.ToWalletId)
                && (t.TransferTypeId == transferTypeId && walletIds.Contains((long)t.ToWalletId)))
                    .Include(s => s.FromWallet.ApplicationUser.UserDetail)
                    .Include(s => s.ToWallet.ApplicationUser.UserDetail)
                    .Include(s => s.TransferType);
            }

            return new TransactionResponseResource
            {
                TotalCount = await transactionQuery.CountAsync(),
                Transactions = await transactionQuery.Skip(skipCount).Take(takeCount).Include(t => t.TransactionStatus).ToListAsync()
            };
        }

        public async Task<IList<TransactionAmountBasedOnMonthsResponseModel>> GetTransactionAmountByMonthLD()
        {
            var excludedTypes = new List<string> { "Top Up System Wallet", "Wlee Tax Transfer", "Cash Out Standard Fee Split", "In hand Credit", "In hand Withdrawl", "Generated Agent Commission", "Cash Out Standard Fee Split" };

            // Fetch the relevant data first
            var transactions = WPContext.Transactions
                .Include(t => t.ToWallet)
                .Where(t => t.Status == 3 && !excludedTypes.Contains(t.TransferType.Name) && t.CreatedDate >= DateTime.Now.AddMonths(-3).Date && t.ToWallet.CurrencyId == 1)
                .ToList();  // Fetch data into memory

            // Perform grouping and conditional sum in memory
            var finalResult = transactions
                .GroupBy(x => new
                {
                    Year = x.CreatedDate.Year,
                    Month = x.CreatedDate.Month
                })
                .Select(g => new TransactionAmountBasedOnMonthsResponseModel
                {
                    Name = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM yyyy"), // Format after grouping
                    Value = g.Count(),
                    TotalAmount_LD = g.Sum(x => x.ToWallet.CurrencyId == 1 ? x.Amount : 0),  // LD sum
                    TotalAmount_USD = g.Sum(x => x.ToWallet.CurrencyId == 2 ? x.Amount : 0),  // USD sum
                })
                .OrderBy(g => g.Name)
                .ToList();

            return finalResult;

        }

        public async Task<IList<TransactionAmountBasedOnMonthsResponseModel>> GetTransactionAmountByMonthUSD()
        {

            var excludedTypes = new List<string> { "Top Up System Wallet", "Wlee Tax Transfer", "Cash Out Standard Fee Split", "In hand Credit", "In hand Withdrawl", "Generated Agent Commission", "Cash Out Standard Fee Split" };

            // Fetch the relevant data first
            var transactions = WPContext.Transactions
                .Include(t => t.ToWallet)
                .Where(t => t.Status == 3 && !excludedTypes.Contains(t.TransferType.Name) && t.CreatedDate >= DateTime.Now.AddMonths(-3).Date && t.ToWallet.CurrencyId == 2)
                .ToList();  // Fetch data into memory

            // Perform grouping and conditional sum in memory
            var finalResult = transactions
                .GroupBy(x => new
                {
                    Year = x.CreatedDate.Year,
                    Month = x.CreatedDate.Month
                })
                .Select(g => new TransactionAmountBasedOnMonthsResponseModel
                {
                    Name = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM yyyy"), // Format after grouping
                    Value = g.Count(),
                    TotalAmount_LD = g.Sum(x => x.ToWallet.CurrencyId == 1 ? x.Amount : 0),  // LD sum
                    TotalAmount_USD = g.Sum(x => x.ToWallet.CurrencyId == 2 ? x.Amount : 0),  // USD sum
                })
                .OrderBy(g => g.Name)
                .ToList();

            return finalResult;

        }

        public async Task<IList<TransactionCountByTypeResponseModel>> GetTransactionCounttByTypeLD()
        {
            var excludedTypes = new List<string> { "Top Up System Wallet", "Wlee Tax Transfer", "Cash Out Standard Fee Split", "In hand Credit", "In hand Withdrawl", "Generated Agent Commission", "Cash Out Standard Fee Split" };

            var transactionCountsByType = WPContext.Transactions.Include(w=>w.ToWallet).Include(t => t.TransferType).Where(t => t.Status == 3 && !excludedTypes.Contains(t.TransferType.Name) && t.FromWallet.CurrencyId == Currencies.LRD.GetHashCode())
            .GroupBy(t => t.TransferType.Name)  // Grouping by transaction type
            .Select(g => new TransactionCountByTypeResponseModel
            {
                Name = g.Key,  // The type of the transaction
                Value = g.Count(),  // Count of transactions for this type
                TotalAmount =Math.Round( g.Sum(t => t.Amount) , 2) // Sum of transaction amounts for this type
            })
            .OrderByDescending(g => g.Value)
            .ToList();

            return transactionCountsByType;

        }

        public async Task<IList<TransactionCountByTypeResponseModel>> GetTransactionCounttByTypeUSD()
        {
            var excludedTypes = new List<string> { "Top Up System Wallet", "Wlee Tax Transfer", "Cash Out Standard Fee Split", "In hand Credit", "In hand Withdrawl", "Generated Agent Commission", "Cash Out Standard Fee Split" };

            var transactionCountsByType = WPContext.Transactions.Include(w => w.ToWallet).Include(t => t.TransferType).Where(t => t.Status == 3 && !excludedTypes.Contains(t.TransferType.Name) && t.FromWallet.CurrencyId == Currencies.USD.GetHashCode())
            .GroupBy(t => t.TransferType.Name)  // Grouping by transaction type
            .Select(g => new TransactionCountByTypeResponseModel
            {
                Name = g.Key,  // The type of the transaction
                Value = g.Count(),  // Count of transactions for this type
                TotalAmount = Math.Round(g.Sum(t => t.Amount), 2)
            })
            .OrderByDescending(g => g.Value)
            .ToList();

            return transactionCountsByType;

        } 
        
        public async Task<IList<TransactionSummaryResponseModel>> GetTransactionData(int monthId, int currencyId)
        {

            var currentDate = DateTime.Now;
            var MonthsAgo = currentDate.AddMonths(-monthId);
            var excludedTypes = new List<string> { "Top Up System Wallet", "Wlee Tax Transfer", "Cash Out Standard Fee Split", "In hand Credit", "In hand Withdrawl", "Generated Agent Commission" };

            var seperateTransactionCountsByMonth = WPContext.Transactions.Include(w => w.ToWallet).Include(t => t.TransferType).Where(t => t.Status == 3 && !excludedTypes.Contains(t.TransferType.Name) && t.CreatedDate >= MonthsAgo && t.FromWallet.CurrencyId == currencyId)
            .GroupBy(t => t.TransferType.Name)  // Grouping by transaction type
            .Select(g => new TransactionSummaryResponseModel
            {
                Name = g.Key,  // The type of the transaction
                Value = g.Count(),  // Count of transactions for this type
                TotalAmount = Math.Round(g.Sum(t => t.Amount), 2)
            })
            .OrderByDescending(g => g.Name)
            .ToList();

            return (seperateTransactionCountsByMonth);
        }

        public async Task<TransactionResponseResource> TransactionCountSummary(int role, int transaferType, DateTime fromDate, DateTime toDate)
        {
            // Fetch the relevant data first
            var transactions = await WPContext.Transactions
                .Include(t => t.ToWallet)
                .Where(t => t.Status == 3 && t.TransferTypeId == transaferType && t.CreatedDate > fromDate && t.CreatedDate < toDate)
                .ToListAsync();

            return new TransactionResponseResource
            {
                TotalCount = transactions.Count(),
                Transactions = transactions
            };

        }
    }
}
