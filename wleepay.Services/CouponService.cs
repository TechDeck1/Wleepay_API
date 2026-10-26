using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Data.Context;

namespace wleepay.Services
{
    public class CouponCodeService : ICouponCodeService
    {
        private readonly WPContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CouponCodeService> _logger;

        public CouponCodeService(WPContext context, IConfiguration configuration, ILogger<CouponCodeService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<CouponListResponseDto> CreateCouponAsync(CouponReqDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.Percentage <= 0 || dto.Percentage > 100)
                throw new ArgumentException("Percentage must be between 1 and 100.", nameof(dto));

            if (dto.ExpirationDate.HasValue && dto.ExpirationDate.Value.Date < DateTime.UtcNow.Date)
                throw new ArgumentException("Expiration date must be in the future.", nameof(dto));

            var existingCoupon = await _context.CouponCodes
                .FirstOrDefaultAsync(c => c.Code == dto.CouponCode);

            if (existingCoupon != null)
                throw new InvalidOperationException($"Coupon with code '{dto.CouponCode}' already exists.");

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            if (string.IsNullOrWhiteSpace(StripeConfiguration.ApiKey))
                throw new InvalidOperationException("Stripe API key is not configured.");

            string stripeCouponId = null;
            string stripePromoId = null;

            try
            {
                var couponOptions = new CouponCreateOptions
                {
                    PercentOff = dto.Percentage,
                    Duration = dto.MaxReedem.HasValue ? "once" : "forever",
                    MaxRedemptions = dto.MaxReedem,
                    RedeemBy = dto.ExpirationDate
                };

                var stripeCouponService = new CouponService();
                var coupon = await stripeCouponService.CreateAsync(couponOptions);
                stripeCouponId = coupon.Id;

                var couponCode = !string.IsNullOrWhiteSpace(dto.CouponCode)
                    ? dto.CouponCode
                    : GenerateCouponCode();

                var promoOptions = new PromotionCodeCreateOptions
                {
                    Promotion = new PromotionCodePromotionOptions
                    {
                        Type = "coupon",
                        Coupon = coupon.Id
                    },
                    Code = couponCode
                };

                var promoService = new PromotionCodeService();
                var promo = await promoService.CreateAsync(promoOptions);
                stripePromoId = promo.Id;

                return await SaveCreatedCouponAsync(dto, couponCode, coupon.Id, promo.Id);
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe error while creating coupon. Code: {Code}", dto.CouponCode);
                await RollbackStripeResourcesAsync(stripePromoId, stripeCouponId);
                throw;
            }
            catch (DbUpdateException)
            {
                await RollbackStripeResourcesAsync(stripePromoId, stripeCouponId);
                throw;
            }
            catch (Exception)
            {
                await RollbackStripeResourcesAsync(stripePromoId, stripeCouponId);
                throw;
            }
        }

        public async Task<PaginatedResponseDto<CouponListResponseDto>> GetAllCouponsAsync(CouponListQueryDto query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            query.Page = Math.Max(1, query.Page);
            query.Limit = Math.Clamp(query.Limit, 1, 100);

            var couponsQuery = _context.CouponCodes
                .Where(c => !c.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                var search = query.Search.Trim().ToLower();
                couponsQuery = couponsQuery.Where(c => c.Code.ToLower().Contains(search));
            }

            couponsQuery = query.SortBy?.ToLower() switch
            {
                "percentoff" => query.SortOrder == "asc" ? couponsQuery.OrderBy(c => c.Percentage) : couponsQuery.OrderByDescending(c => c.Percentage),
                "expireson" => query.SortOrder == "asc" ? couponsQuery.OrderBy(c => c.ExpirationDate) : couponsQuery.OrderByDescending(c => c.ExpirationDate),
                "redeemed" => query.SortOrder == "asc" ? couponsQuery.OrderBy(c => c.Redeemed) : couponsQuery.OrderByDescending(c => c.Redeemed),
                "createdat" => query.SortOrder == "asc" ? couponsQuery.OrderBy(c => c.CreatedAt) : couponsQuery.OrderByDescending(c => c.CreatedAt),
                _ => couponsQuery.OrderByDescending(c => c.CreatedAt)
            };

            var totalRecords = await couponsQuery.CountAsync();
            var totalPages = totalRecords == 0 ? 0 : (int)Math.Ceiling(totalRecords / (double)query.Limit);

            var coupons = await couponsQuery
                .Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .Select(c => new CouponListResponseDto
                {
                    CouponCode = c.Code,
                    PercentOff = c.Percentage,
                    ExpiresOn = c.ExpirationDate,
                    Redeemed = c.Redeemed,
                    IsExposed = c.IsExposed,
                    MaxRedemptions = c.MaxRedemptions,
                    CreatedDate = c.CreatedAt
                })
                .ToListAsync();

            return new PaginatedResponseDto<CouponListResponseDto>
            {
                Data = coupons,
                TotalRecords = totalRecords,
                Page = query.Page,
                Limit = query.Limit,
                TotalPages = totalPages
            };
        }

        public async Task<List<CouponListResponseDto>> HandleGetAllCouponAsync()
        {
            return await _context.CouponCodes
                .Where(c => !c.IsDeleted)
                .Select(c => new CouponListResponseDto
                {
                    CouponCode = c.Code,
                    PercentOff = c.Percentage,
                    ExpiresOn = c.ExpirationDate,
                    Redeemed = c.Redeemed,
                    MaxRedemptions = c.MaxRedemptions,
                    CreatedDate = c.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<bool> DeleteCouponAsync(string couponCode)
        {
            try
            {
                StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

                if (string.IsNullOrWhiteSpace(StripeConfiguration.ApiKey))
                    throw new InvalidOperationException("Stripe API key is not configured.");

                var coupon = await _context.CouponCodes
                    .FirstOrDefaultAsync(c => c.Code == couponCode);

                if (coupon == null)
                    return false;

                var stripeCouponService = new CouponService();
                await stripeCouponService.DeleteAsync(coupon.StripeCouponId);

                coupon.IsDeleted = true;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateVisibilityAsync(string code, bool isExposed, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(code))
                return false;

            var normalized = code.Trim();

            var coupon = await _context.Set<CouponCode>()
                .FirstOrDefaultAsync(c => c.Code == normalized && !c.IsDeleted, cancellationToken);

            if (coupon == null)
                return false;

            if (coupon.IsExposed != isExposed)
            {
                coupon.IsExposed = isExposed;
                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }

        public async Task<List<CouponCode>> GetVisibleCouponsAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            return await _context.Set<CouponCode>()
                .AsNoTracking()
                .Where(c => c.IsExposed
                            && !c.IsDeleted
                            && (c.ExpirationDate == null || c.ExpirationDate > now)
                            && (c.MaxRedemptions == null || c.Redeemed < c.MaxRedemptions))
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        private static string GenerateCouponCode(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }

            return new string(result);
        }

        private async Task RollbackStripeResourcesAsync(string stripePromoId, string stripeCouponId)
        {
            try
            {
                if (stripePromoId != null)
                {
                    var promoService = new PromotionCodeService();
                    await promoService.UpdateAsync(stripePromoId, new PromotionCodeUpdateOptions { Active = false });
                }

                if (stripeCouponId != null)
                {
                    var stripeCouponService = new CouponService();
                    await stripeCouponService.DeleteAsync(stripeCouponId);
                }
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Failed to roll back Stripe resources.");
            }
        }

        private async Task<CouponListResponseDto> SaveCreatedCouponAsync(CouponReqDto dto, string couponCode, string stripeCouponId, string promoId)
        {
            if (string.IsNullOrWhiteSpace(couponCode))
                throw new ArgumentException("Coupon code cannot be empty.", nameof(couponCode));

            if (string.IsNullOrWhiteSpace(stripeCouponId))
                throw new ArgumentException("Stripe coupon ID cannot be empty.", nameof(stripeCouponId));

            if (string.IsNullOrWhiteSpace(promoId))
                throw new ArgumentException("Stripe promo ID cannot be empty.", nameof(promoId));

            var coupon = new CouponCode
            {
                Id = Guid.NewGuid(),
                Code = couponCode,
                IsExposed = dto.IsExposed,
                DiscountType = DiscountTypes.Percentage,
                Percentage = dto.Percentage,
                StripeCouponId = stripeCouponId,
                StripePromoCodeId = promoId,
                MaxRedemptions = dto.MaxReedem,
                ExpirationDate = dto.ExpirationDate,
                CreatedAt = DateTime.UtcNow
            };

            _context.CouponCodes.Add(coupon);
            await _context.SaveChangesAsync();

            return new CouponListResponseDto
            {
                CouponCode = coupon.Code,
                PercentOff = coupon.Percentage,
                ExpiresOn = coupon.ExpirationDate,
                Redeemed = coupon.Redeemed,
                MaxRedemptions = coupon.MaxRedemptions,
                CreatedDate = coupon.CreatedAt
            };
        }
    }
}