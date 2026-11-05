using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

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


        // for  web app
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

        // for mobile app
        public async Task<MobileTransactionPage> GetMobileTransactionsAsync(string userId, string? kind, int pageNumber, int pageSize)
        {
            var baseQuery = _context.UserTransactions.AsNoTracking()
                .Where(t => t.UserId == userId
                            && t.Status == "Success"
                            && MobileTypeIds.Contains(t.TransferTypeId));

            // Totals per kind over the whole history, independent of paging and filter
            var sums = await baseQuery
                .GroupBy(t => t.TransferTypeId)
                .Select(g => new { g.Key, Sum = g.Sum(x => x.Amount) })
                .ToListAsync();

            var totals = new Dictionary<string, decimal> { ["airtime"] = 0, ["utility"] = 0, ["voucher"] = 0 };
            foreach (var s in sums)
                if (KindOf(s.Key) is { } k) totals[k] = s.Sum;

            var listQuery = baseQuery;
            if (!string.IsNullOrEmpty(kind))
            {
                var typeId = kind switch { "airtime" => 10, "utility" => 11, "voucher" => 17, _ => -1 };
                listQuery = listQuery.Where(t => t.TransferTypeId == typeId);
            }

            var totalCount = await listQuery.CountAsync();
            var rows = await listQuery
                .OrderByDescending(t => t.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var page = new MobileTransactionPage { TotalCount = totalCount, Totals = totals };
            if (rows.Count == 0) return page;

            // One query for every possible Sochitel match on this page
            var ids = rows.Select(r => r.TransactionId).ToList();
            var phones = rows.Select(r => r.ReceiverPhoneNumber).Where(p => !string.IsNullOrEmpty(p)).Distinct().ToList();
            var from = rows.Min(r => r.CreatedAt).AddMinutes(-2);
            var to = rows.Max(r => r.CreatedAt).AddMinutes(2);

            var candidates = await _context.SochitelTransactions.AsNoTracking()
                .Where(s => ids.Contains(s.UserReference) || ids.Contains(s.TransactionId)
                            || (s.CreatedDate >= from && s.CreatedDate <= to
                                && (phones.Contains(s.PhoneNumber) || s.PhoneNumber == null || s.PhoneNumber == "")))
                .ToListAsync();

            var used = new HashSet<int>();

            foreach (var t in rows)
            {
                var k = KindOf(t.TransferTypeId)!;

                // 1) exact link via payment intent id, 2) closest in time within 2 minutes
                var s = candidates.FirstOrDefault(c => c.UserReference == t.TransactionId || c.TransactionId == t.TransactionId)
                    ?? candidates
                        .Where(c => !used.Contains(c.Id)
                                    && c.CreatedDate.HasValue
                                    && SochitelKind(c.TransactionType) == k
                                    && (string.IsNullOrEmpty(t.ReceiverPhoneNumber)
                                            ? string.IsNullOrEmpty(c.PhoneNumber)
                                            : c.PhoneNumber == t.ReceiverPhoneNumber)
                                    && (ParseAmount(c.TransactionAmount) ?? 0) <= t.Amount
                                    && Math.Abs((c.CreatedDate!.Value - t.CreatedAt).TotalSeconds) <= 120)
                        .OrderBy(c => Math.Abs((c.CreatedDate!.Value - t.CreatedAt).TotalSeconds))
                        .FirstOrDefault();

                if (s != null) used.Add(s.Id);

                var baseAmount = ParseAmount(s?.TransactionAmount) ?? t.Amount;
                var meter = !string.IsNullOrEmpty(s?.MeterNumber)
                    ? s!.MeterNumber
                    : s?.ExtraInfo is { } info && MeterRegex.Match(info) is { Success: true } m ? m.Groups[1].Value : null;

                var provider = s?.OperatorId != null && OperatorNames.TryGetValue(s.OperatorId, out var name)
                    ? name : KindLabels[k];

                page.Data.Add(new MobileTransactionDto
                {
                    Id = t.TransactionId,
                    Kind = k,
                    Product = $"${baseAmount:0.##} {KindLabels[k]}",   // CurrencyId 2 assumed USD
                    Provider = provider,
                    Receiver = k switch
                    {
                        "airtime" => t.ReceiverPhoneNumber ?? "",
                        "utility" => meter != null ? $"Meter {meter}" : (t.ReceiverPhoneNumber ?? ""),
                        _ => "Self"
                    },
                    CreatedAt = DateTime.SpecifyKind(t.CreatedAt, DateTimeKind.Utc), // assumes UTC storage
                    Total = t.Amount,
                    Fee = t.Amount - baseAmount,
                    Draft = new MobileDraftDto
                    {
                        Kind = k,
                        OperatorId = s?.OperatorId,
                        ProductId = s?.ProductId,
                        ReceiverPhone = t.ReceiverPhoneNumber,
                        MeterNumber = meter,
                        Amount = baseAmount
                    }
                });
            }

            return page;
        }

        private static readonly int[] MobileTypeIds = { 10, 11, 17 };
        private static string? KindOf(int transferTypeId) => transferTypeId switch
        {
            10 => "airtime",
            11 => "utility",
            17 => "voucher",
            _ => null
        };

        private static string? SochitelKind(string? type) =>
            type == null ? null :
            type.StartsWith("Mobile Recharge") ? "airtime" :
            type.StartsWith("Bill Payment") ? "utility" :
            type.StartsWith("Gift Voucher") ? "voucher" : null;

        // Fill from your operator list. Unknown ids fall back to a generic label.
        private static readonly Dictionary<string, string> OperatorNames = new Dictionary<string, string>
        {
            // ["10"] = "...", ["151"] = "...", ["2470"] = "...", ["2482"] = "...",
        };

        private static readonly Dictionary<string, string> KindLabels = new Dictionary<string, string>
        {
            ["airtime"] = "Airtime",
            ["utility"] = "Electricity",
            ["voucher"] = "Gift voucher"
        };

        private static readonly Regex MeterRegex = new Regex(@"Meter Number:\s*(\d+)", RegexOptions.Compiled);
        private static decimal? ParseAmount(string? value) => decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? d : (decimal?)null;
    }
}
