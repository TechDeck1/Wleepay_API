using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using wleepay.Core.DTO.Request;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface ICouponCodeService
    {
        Task<CouponListResponseDto> CreateCouponAsync(CouponReqDto dto);

        Task<PaginatedResponseDto<CouponListResponseDto>> GetAllCouponsAsync(CouponListQueryDto query);

        Task<List<CouponListResponseDto>> HandleGetAllCouponAsync();

        Task<bool> UpdateVisibilityAsync(string code, bool isExposed, CancellationToken cancellationToken = default);

        Task<List<CouponCode>> GetVisibleCouponsAsync(CancellationToken cancellationToken = default);

        Task<bool> DeleteCouponAsync(string couponCode);
    }
}