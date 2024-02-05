using System;
using System.Collections.Generic;
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
    }
}
