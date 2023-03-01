using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class AspNetUserConfiguration : IEntityTypeConfiguration<AspNetUser>
    {
        public void Configure(EntityTypeBuilder<AspNetUser> builder)
        {
            builder
                .ToTable("AspNetUsers");

            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.UserName);

            builder
                .Property(c => c.NormalizedUserName);

            builder
                .Property(c => c.Email);

            builder
                .Property(c => c.NormalizedEmail);

            builder
                .Property(c => c.EmailConfirmed)
                .IsRequired();

            builder
                .Property(c => c.PasswordHash);

            builder
                .Property(c => c.SecurityStamp);

            builder
                .Property(c => c.ConcurrencyStamp);

            builder
                .Property(c => c.PhoneNumberConfirmed);

            builder
                .Property(c => c.TwoFactorEnabled)
                .IsRequired();

            builder
                .Property(c => c.LockoutEnd);

            builder
                .Property(c => c.LockoutEnabled)
                .IsRequired();

            builder
                .Property(c => c.AccessFailedCount)
                .IsRequired();
        }
    }
}
