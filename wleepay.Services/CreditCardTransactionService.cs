using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class CreditCardTransactionService : ICreditCardTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreditCardTransactionService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreditCardTransactions> AddCreditCardTransactions(CreditCardTransactions creditCardTransactions)
        {
            return await _unitOfWork.CreditCardTransaction.AddCreditCardTransaction(creditCardTransactions);


        }
    }
}
