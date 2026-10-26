using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace wleepay.Core.Models
{
    public enum DiscountTypes
    {
        Percentage = 0,
        FixedAmount = 1
    }

    [Table("CouponCode")]
    public class CouponCode
    {
        public Guid Id { get; set; }
        public  string Code { get; set; }
        public DiscountTypes DiscountType { get; set; }
        public int Percentage { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? MaxRedemptions { get; set; } = null;
        public int Redeemed { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public string? AppliedBy { get; set; }
        public  string StripeCouponId { get; set; } 
        public string StripePromoCodeId { get; set; }
        public bool IsExposed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
