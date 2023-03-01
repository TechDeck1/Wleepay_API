using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class SmileIdentityJobConfiguration : IEntityTypeConfiguration<SmileIdentityJob>
    {
        public void Configure(EntityTypeBuilder<SmileIdentityJob> builder)
        {
            builder
                .ToTable("SmileIdentityJob");

            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.SecKey)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(c => c.UserId)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(c => c.PartnerId)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(c => c.ApplicationUserId)
                .HasMaxLength(450)
                .IsRequired();

            builder
                .Property(c => c.LastStatus)
                .HasMaxLength(10);

            builder
                .Property(c => c.TimeStamp)
                .HasMaxLength(50);

            builder
                .HasOne(c => c.ApplicationUser)
                .WithMany(c => c.SmileIdentityJobs)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(c => c.ApplicationUserId);
        }
    }
}
