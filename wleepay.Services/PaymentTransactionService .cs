using AutoMapper;
using Google;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.DTO.Request;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Data.Context;


namespace wleepay.Services
{
    public class PaymentTransactionService : IPaymentTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PaymentTransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task SaveTransactionAsync(PaymentTransactionDto dto)
        {
            var entity = _mapper.Map<PaymentTransaction>(dto);
            await _unitOfWork.PaymentTransactions.SaveTransactionAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<PaymentTransaction> GetByTransactionIdAsync(string transactionId)
        {
            return await _unitOfWork.PaymentTransactions.GetByTransactionIdAsync(transactionId);
        }

        public async Task<int> GetTransactionCountAsync()
        {
            return await _unitOfWork.PaymentTransactions.GetTransactionCountAsync();
        }
        public async Task<decimal> GetTotalTransactionVolumeAsync()
        {
            return await _unitOfWork.PaymentTransactions.GetTotalTransactionVolumeAsync();
        }

        public async Task<PaginatedTransactionResult<TransactionHistory>> GetTransactionsByUserIdAsync(string userId, int? transferTypeId, int pageNumber, int pageSize)
        {
            return await _unitOfWork.PaymentTransactions.GetTransactionsByUserIdAsync(userId, transferTypeId, pageNumber, pageSize);
        }

        public async Task<PaginatedTransactionResult<TransactionReport>> GetTransactionsByTypeAndDateRange(int transferTypeId, DateTime startDate, DateTime endDate,
            int pageNumber, int pageSize)
        {
            return await _unitOfWork.PaymentTransactions.GetTransactionsByTypeAndDateRange(transferTypeId, startDate, endDate, pageNumber, pageSize);
        }

        public async Task<MobileTransactionPage> GetMobileTransactionsAsync(string userId, string kind, int pageNumber, int pageSize)
        {
            return await _unitOfWork.PaymentTransactions.GetMobileTransactionsAsync(userId, kind, pageNumber, pageSize);
        }

    }
}
