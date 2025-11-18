using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;



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
            var transactionRsponse = await WPContext.SochitelTransactions.AddAsync(transaction);
            WPContext.SaveChanges();
            return transaction;
        }


        public async Task<double> GetTotalCommissionEarnedAsync(DateTime fromDate)
        {
            var list = await WPContext.SochitelTransactions
                .Where(t => t.CreatedDate >= fromDate)
                .ToListAsync();

            double total = 0;

            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.CommissionRate))
                {
                    double value;
                    if (double.TryParse(item.CommissionRate, out value))
                    {
                        total += value;
                    }
                }
            }

            return total;
        }



    }

}
