using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class CouponCodeConfiguration : IEntityTypeConfiguration<CouponCode>
    {
        public void Configure(EntityTypeBuilder<CouponCode> builder)
        {
            builder.ToTable("CouponCode");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Code)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(c => c.Code)
                   .IsUnique();

            builder.Property(c => c.DiscountType)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(c => c.Percentage)
                   .IsRequired();

            builder.Property(c => c.ExpirationDate);

            builder.Property(c => c.MaxRedemptions);

            builder.Property(c => c.Redeemed)
                   .HasDefaultValue(0);

            builder.Property(c => c.IsDeleted)
                   .HasDefaultValue(false);

            builder.Property(c => c.IsExposed)
                   .HasDefaultValue(false);

            builder.Property(c => c.AppliedBy)
                   .HasMaxLength(200);

            builder.Property(c => c.StripeCouponId)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.StripePromoCodeId)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}