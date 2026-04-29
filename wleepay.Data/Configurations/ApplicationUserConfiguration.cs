using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder
                 .Ignore(c => c.PhoneNumber);

            builder.HasData(new ApplicationUser
            {
                Id = "07ac193d-26e7-4039-9440-fbb53988b878",
                UserName = "wleeadmin@wleepay.com",
                NormalizedEmail = "WLEEADMIN@WLEEPAY.COM",
                NormalizedUserName = "WLEEADMIN@WLEEPAY.COM",
                Email = "wleeadmin@wleepay.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "11118888wleePay@"),
                SecurityStamp = Guid.NewGuid().ToString("D"),
            });
        }
    }
}
