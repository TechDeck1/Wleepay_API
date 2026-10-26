using global::wleepay.Core.Models;
using global::wleepay.Core.Repositories;
using global::wleepay.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace wleepay.Data.Repositories
{

        public class CouponCodeRepository : ICouponCodeRepository
        {
            private readonly WPContext _context;

            public CouponCodeRepository(WPContext context)
            {
                _context = context;
            }

            public async Task<CouponCode?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
            {
                if (string.IsNullOrWhiteSpace(code))
                    return null;

                var normalized = code.Trim();

                return await _context.Set<CouponCode>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Code == normalized && !c.IsDeleted, cancellationToken);
            }

            public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
            {
                var coupon = await _context.Set<CouponCode>()
                    .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted, cancellationToken);

                if (coupon == null)
                    return false;

                coupon.IsDeleted = true;
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }
}
