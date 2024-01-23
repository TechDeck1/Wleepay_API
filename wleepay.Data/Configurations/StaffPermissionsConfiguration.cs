using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class StaffPermissionsConfiguration : IEntityTypeConfiguration<StaffPermissions>
    {
        public void Configure(EntityTypeBuilder<StaffPermissions> builder)
        {
            builder
              .ToTable("StaffPermissions");

            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .UseIdentityColumn();

            builder.Property(u=> u.ApplicationUserId).IsRequired();

            builder.Property(u => u.PermissionId).IsRequired();

            builder
                .HasOne(u => u.PermissionType)
                .WithMany(u => u.StaffPermissions)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(u => u.PermissionId);

            builder
                .HasOne(u => u.ApplicationUser)
                .WithMany(u => u.StaffPermissions)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(u => u.ApplicationUserId);


        }
    }
}
