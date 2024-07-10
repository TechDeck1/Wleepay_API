using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;
using System.Reflection.Emit;

namespace wleepay.Data.Configurations
{
    public class UserDevicesConfiguration : IEntityTypeConfiguration<UserDevices>
    {
        public void Configure(EntityTypeBuilder<UserDevices> builder)
        {
            builder
                .ToTable("UserDevices");

            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .UseIdentityColumn();

            builder
                .Property(u => u.UserId)
                .HasMaxLength(450)
                .IsRequired();

            builder
                .Property(u => u.DeviceId)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(u => u.FcmToken)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(u => u.AddedDate);
            
            builder
                .Property(u => u.IsActive);

            builder
                 .HasOne(u =>u.ApplicationUser )
                 .WithMany(u => u.UserDevices)
                 .OnDelete(DeleteBehavior.ClientNoAction)
                 .HasForeignKey(u => u.UserId);

        }
    }
}
