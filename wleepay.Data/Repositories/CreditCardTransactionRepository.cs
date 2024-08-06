using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    internal class CreditCardTransactionRepository : Repository<SochitelTransactions>, ICreditCardTransactionRepository
    {

        public CreditCardTransactionRepository(WPContext context)
           : base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<CreditCardTransactions> AddCreditCardTransaction(CreditCardTransactions transaction)
        {
            await WPContext.CreditCardTransactions.AddAsync(transaction);
            WPContext.SaveChanges();
            return transaction;
        }
    }
}
