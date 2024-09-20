using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder
                .ToTable("Currencies");

            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .UseIdentityColumn();

            builder
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(c => c.Symbol)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasData(new Currency
            {
                Id = 1,
                Name = "LD",
                Symbol = "L$"
            });
            
            builder.HasData(new Currency
            {
                Id = 2,
                Name = "USD",
                Symbol = "$"
            });
        }
    }
}
