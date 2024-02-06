using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    public class CashFlowHistoryRepository: Repository<CashFlowHistory>, ICashFlowHistoryRepository
    {
        public CashFlowHistoryRepository(WPContext context)
        : base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<CashFlowHistory> AddCashFlowHistoryAsync(CashFlowHistory cashFlowHistory)
        {
            using (IDbContextTransaction transaction = WPContext.Database.BeginTransaction())
            {
                try
                {
                   
                    //create new transaction record or update cash flow historry for payment request
                    if (cashFlowHistory.WalletId > 0)
                    {
                        cashFlowHistory.CreatedDate = DateTime.Now;
                        await WPContext.CashFlowHistory.AddAsync(cashFlowHistory);
                    }

                    await WPContext.SaveChangesAsync();
                  
                    //update receiver wallet balance
                    var receiverWallet = await WPContext.Wallet.Where(w => w.Id == cashFlowHistory.WalletId).FirstOrDefaultAsync();
                    receiverWallet.Balance = ((double)(receiverWallet.Balance + cashFlowHistory.DepositAmount));

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

            var newAddedCashFlowHistory = await WPContext.CashFlowHistory
                .Where(w => w.Id == cashFlowHistory.Id)
                .Include(w => w.toWallet)
                .Include(w => w.TransactionStatus)
                .FirstOrDefaultAsync();

            return newAddedCashFlowHistory;



            //return cashFlowHistory;
        }

        public async Task<IEnumerable<CashFlowHistory>> GetAllCashFlowHistoryAsync()
        {
            var cashFlowHistory = await WPContext.CashFlowHistory.OrderByDescending(d => d.CreatedDate).ToListAsync();
            return cashFlowHistory;
        }

        public async Task<CashFlowHistory> GetLatestCashFlowHistoryAsync()
        {
            var cashFlowHistory= await WPContext.CashFlowHistory.OrderByDescending(d=> d.CreatedDate).FirstOrDefaultAsync();
            return cashFlowHistory;
        }
    }
}
