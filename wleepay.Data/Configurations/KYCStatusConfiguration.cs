using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class KYCStatusConfiguration : IEntityTypeConfiguration<KYCStatus>
    {
        public void Configure(EntityTypeBuilder<KYCStatus> builder)
        {
            builder
                .ToTable("KYCStatus");

            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .UseIdentityColumn();

            builder
                .Property(c => c.Name)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasData(new KYCStatus
            {
                Id = 1,
                Name = "Pending"
            });

            builder.HasData(new KYCStatus
            {
                Id = 2,
                Name = "Approved"
            });

            builder.HasData(new KYCStatus
            {
                Id = 3,
                Name = "Rejected"
            });

            builder.HasData(new KYCStatus
            {
                Id = 4,
                Name = "Under Review"
            });
        }
    }
}
