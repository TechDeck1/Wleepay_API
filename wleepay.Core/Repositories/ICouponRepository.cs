using System;
using System.Threading;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface ICouponCodeRepository
    {

        Task<CouponCode?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
