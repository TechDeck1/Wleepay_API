using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class KYCLevelConfiguration : IEntityTypeConfiguration<KYCLevel>
    {
        public void Configure(EntityTypeBuilder<KYCLevel> builder)
        {
            builder
                .ToTable("KYCLevel");

            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .UseIdentityColumn();

            builder
                .Property(c => c.Name)
                .HasMaxLength(10)
                .IsRequired();

            builder
                .Property(c => c.AccountBalanceLimit);

            builder
                .Property(c => c.TransactionDayLimit);

            builder
                .Property(c => c.TransactionMonthLimit);

            builder
                .Property(c => c.Description);

            builder.HasData(new KYCLevel
            {
                Id = 1,
                Name = "L1",
                Description = null,
                AccountBalanceLimit = 1000,
                TransactionMonthLimit = 2000,
                TransactionDayLimit = 250
            });

            builder.HasData(new KYCLevel
            {
                Id = 2,
                Name = "L2",
                Description = null,
                AccountBalanceLimit = 4000,
                TransactionMonthLimit = 8000,
                TransactionDayLimit = 1000
            });

            builder.HasData(new KYCLevel
            {
                Id = 3,
                Name = "L3",
                Description = null,
                AccountBalanceLimit = null,
                TransactionMonthLimit = null,
                TransactionDayLimit = null
            });

        }
    }
}
