using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wleepay.Core.DTO.Response
{

        public class CouponReqDto
        {
            [Required]
            [Range(1, 100, ErrorMessage = "Percentage must be between 1 and 100.")]
            public int Percentage { get; set; }

            [StringLength(20, MinimumLength = 3, ErrorMessage = "Coupon code must be between 3 and 20 characters.")]
            public string? CouponCode { get; set; }
            public  bool IsExposed { get; set; }
            public int? MaxReedem { get; set; } = null;
            public DateTime? ExpirationDate { get; set; }
        }

        public class CouponListQueryDto
        {

            public string? Search { get; set; } = string.Empty;
            public int Page { get; set; } = 1;
            public int Limit { get; set; } = 10;
            public string? SortBy { get; set; } = "CreatedAt";
            public string? SortOrder { get; set; } = "desc";
        }

        public class CouponListResponseDto
        {
            public string CouponCode { get; set; } = string.Empty;
            public int? PercentOff { get; set; }
            public DateTime? ExpiresOn { get; set; } = null;
            public int? MaxRedemptions { get; set; } = null;
            public int Redeemed { get; set; }
            public  bool  IsExposed { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public class PaginatedResponseDto<T>
        {
            public List<T> Data { get; set; }
            public int TotalRecords { get; set; }
            public int Page { get; set; }
            public int Limit { get; set; }
            public int TotalPages { get; set; }
    }


        public class UpdateCouponVisibilityDto
        {
            [Required(ErrorMessage = "Coupon code is required.")]
            public string CouponCode { get; set; } = string.Empty;

            [Required(ErrorMessage = "Status is required.")]
            public bool? Status { get; set; }
    }

        public class PublicCouponDto
        {
            public string Code { get; set; } = string.Empty;
            public string StripePromoCodeId { get; set; } = string.Empty;
            public int Percentage { get; set; }
            public DateTime CreatedAt { get; set; }
        }
}
