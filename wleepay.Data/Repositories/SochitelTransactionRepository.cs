using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    public class SochitelTransactionRepository : Repository<SochitelTransactions>, ISochitelTransactionRepository
    {

        public SochitelTransactionRepository(WPContext context)
           : base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<SochitelTransactions> AddSochitelTransaction(SochitelTransactions transaction)
        {
            await WPContext.SochitelTransactions.AddAsync(transaction);
            WPContext.SaveChanges();
            return transaction;
        }
    }
    
}
