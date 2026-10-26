using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.StripeProductId)
                   .HasMaxLength(100);

            builder.Property(p => p.SochitelProductId)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(p => p.SochitelProductId)
                   .IsUnique();

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(p => p.Type)
                   .HasMaxLength(20);

            builder.Property(p => p.Category)
                   .HasMaxLength(20);

            builder.Property(p => p.PriceType)
                   .HasMaxLength(20);

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.PriceMin)
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.PriceMax)
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Currency)
                   .HasMaxLength(10);

            builder.Property(p => p.SochitelOperatorId)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(p => p.SochitelOperatorId);

            builder.Property(p => p.IsDeleted)
                   .HasDefaultValue(false);

            builder.Property(p => p.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.LastSyncedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}