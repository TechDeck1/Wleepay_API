using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;
using static DevExpress.XtraPrinting.Native.PageSizeInfo;

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

                    //await WPContext.SaveChangesAsync();
                  
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
                .FirstOrDefaultAsync(); //no query

            return newAddedCashFlowHistory;

        }

        public async Task<CashFlowHistory> AddCashFlowHistoryForWithdraw(CashFlowHistory cashFlowHistory)
        {
            //create new transaction record or update cash flow historry for payment request
            if (cashFlowHistory.WalletId > 0)
            {
                cashFlowHistory.CreatedDate = DateTime.Now;
                await WPContext.CashFlowHistory.AddAsync(cashFlowHistory);
            }

            await WPContext.SaveChangesAsync();

            return cashFlowHistory;
        }

        public async Task<CashFlowHistoryResponse> GetAllCashFlowHistoryAsync(int currencyId, int index, int size)
        {
            int takeCount = size;
            int skipCount = index * size;
            var cashFlowHistory = await WPContext.CashFlowHistory.Where(x=>x.CurrencyId==currencyId).OrderByDescending(d => d.CreatedDate)
                .Skip(skipCount)
                .Take(takeCount)
                .ToListAsync();
          
            return new CashFlowHistoryResponse
            {
                TotalCount = await WPContext.CashFlowHistory.Where(x => x.CurrencyId == currencyId).CountAsync(),
                CashFlowHistory=cashFlowHistory
            };
        }

        public async Task<CashFlowHistory> GetLatestCashFlowHistoryAsync(long walletId)
        {
            var cashFlowHistory= await WPContext.CashFlowHistory.Where(x => x.WalletId == walletId).OrderByDescending(d=> d.CreatedDate).FirstOrDefaultAsync();
            return cashFlowHistory;
        }
    }
}
