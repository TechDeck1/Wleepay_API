using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class StaffPermissionTypeConfiguration : IEntityTypeConfiguration<StaffPermissionType>
    {
        public void Configure(EntityTypeBuilder<StaffPermissionType> builder)
        {
            builder
               .ToTable("StaffPermissionType");

            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .UseIdentityColumn();


            builder.Property(u => u.Name);


            builder.HasData(new StaffPermissionType
            {
                Id = 1,
                Name = "System Wallet",


            });

            builder.HasData(new StaffPermissionType
            {
                Id = 2,
                Name = "Agent Management",


            });

            builder.HasData(new StaffPermissionType
            {
                Id = 3,
                Name = "Subscriber Management",


            });

            builder.HasData(new StaffPermissionType
            {
                Id = 4,
                Name = "Customer Service",


            });

            builder.HasData(new StaffPermissionType
            {
                Id = 5,
                Name = "Reports",


            });

            builder.HasData(new StaffPermissionType
            {
                Id = 6,
                Name = "Compliance & Risk Management",


            });

            builder.HasData(new StaffPermissionType
            {
                Id = 7,
                Name = "System Admin",

            });



        }
    }
}
