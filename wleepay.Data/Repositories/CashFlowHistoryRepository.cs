using DevExpress.Xpo;
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
            cashFlowHistory.CreatedDate = DateTime.Now; 
            await WPContext.CashFlowHistory.AddAsync(cashFlowHistory);
            WPContext.SaveChanges();

            return cashFlowHistory;
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
