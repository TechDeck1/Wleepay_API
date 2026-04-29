using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;
using AutoMapper;

namespace wleepay.Data.Repositories
{
    public class PaymentTransactionRepository : IPaymentTransactionRepository
    {
        private readonly WPContext _context;
        private readonly IMapper _mapper;
        public PaymentTransactionRepository(WPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task SaveTransactionAsync(PaymentTransaction transaction)
        {
            if (string.IsNullOrEmpty(transaction.TransactionId))
                return;

            var existing = await _context.UserTransactions.FirstOrDefaultAsync(t => t.TransactionId == transaction.TransactionId);

            if (existing != null)
            {
                //existing.Status = transaction.Status;
                //existing.Amount = transaction.Amount;
                //existing.ReceiverPhoneNumber = transaction.ReceiverPhoneNumber;
                //existing.TransferTypeId = transaction.TransferTypeId;
                //existing.CreatedAt = DateTime.UtcNow;
                //existing.Street = transaction.Street;
                //existing.City = transaction.City;
                //existing.ZipCode = transaction.ZipCode;
                _mapper.Map(transaction, existing);
                existing.CreatedAt = DateTime.UtcNow;

                _context.UserTransactions.Update(existing);
            }
            else
            {
                transaction.CreatedAt = DateTime.UtcNow;
                await _context.UserTransactions.AddAsync(transaction);
            }
        }

        public async Task<PaymentTransaction> GetByTransactionIdAsync(string transactionId)
        {
            return await _context.UserTransactions.FirstOrDefaultAsync(t => t.TransactionId == transactionId);
        }

        public async Task<int> GetTransactionCountAsync()
        {
            return await _context.UserTransactions.CountAsync();
        }

        public async Task<decimal> GetTotalTransactionVolumeAsync()
        {
            return await _context.UserTransactions
                .Where(t => t.Status == "Success")
                .SumAsync(t => t.Amount);
        }

        public async Task<PaginatedTransactionResult<TransactionHistory>> GetTransactionsByUserIdAsync(string userId, int? transferTypeId, int pageNumber, int pageSize)
        {
            var query = from t in _context.UserTransactions
                        join tt in _context.TransferTypes on t.TransferTypeId equals tt.Id
                        where t.UserId == userId
                        orderby t.CreatedAt descending
                        select new { t, tt.Name };

            if (transferTypeId.HasValue)
                query = query.Where(x => x.t.TransferTypeId == transferTypeId.Value);

            var totalCount = await query.CountAsync();
            var rawData = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var windowMinutes = 60;
            var minCreatedAt = rawData.Count == 0 ? DateTime.UtcNow : rawData.Min(x => x.t.CreatedAt).AddMinutes(-windowMinutes);
            var maxCreatedAt = rawData.Count == 0 ? DateTime.UtcNow : rawData.Max(x => x.t.CreatedAt).AddMinutes(windowMinutes);
            var phoneNumbers = rawData.Select(x => x.t.ReceiverPhoneNumber).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();

            var sochitelCandidates = await _context.SochitelTransactions
                .Where(s => phoneNumbers.Contains(s.PhoneNumber)
                            && s.CreatedDate >= minCreatedAt
                            && s.CreatedDate <= maxCreatedAt)
                .ToListAsync();

            var data = rawData.Select(item =>
            {
                var mapped = _mapper.Map<TransactionHistory>(item.t);
                mapped.TransferTypeName = item.Name;

                var closest = sochitelCandidates
                    .Where(s => s.PhoneNumber == item.t.ReceiverPhoneNumber)
                    .OrderBy(s => Math.Abs((s.CreatedDate.GetValueOrDefault() - item.t.CreatedAt).TotalSeconds))
                    .FirstOrDefault();

                  mapped.OperatorId = closest?.OperatorId;
                  mapped.ProductId = closest?.ProductId;
                  mapped.MeterNumber = closest?.MeterNumber;
                  return mapped;
              }).ToList();

            return new PaginatedTransactionResult<TransactionHistory>
            {
                TotalCount = totalCount,
                Data = data
            };
        }


        public async Task<PaginatedTransactionResult<TransactionReport>> GetTransactionsByTypeAndDateRange(int transferTypeId, DateTime startDate, DateTime endDate, int pageNumber, int pageSize)
        {
            var query = from t in _context.UserTransactions
                        join u in _context.UserDetail on t.UserId equals u.UserId
                        where t.TransferTypeId == transferTypeId &&
                              t.CreatedAt >= startDate && t.CreatedAt <= endDate.AddDays(1)
                        orderby t.CreatedAt descending
                        select new { t, u.FirstName, u.LastName };

            var totalCount = await query.CountAsync();
            var rawData = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var data = rawData.Select(item =>
            {
                var dto = _mapper.Map<TransactionReport>(item.t);
                dto.Name = $"{item.FirstName} {item.LastName}";
                dto.Address = string.Join(", ", new[] { item.t.Street, item.t.City }.Where(x => !string.IsNullOrEmpty(x)));
                dto.Date = item.t.CreatedAt;
                return dto;
            }).ToList();

            return new PaginatedTransactionResult<TransactionReport>
            {
                TotalCount = totalCount,
                Data = data
            };
        }
    }
}
