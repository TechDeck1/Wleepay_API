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
                Name = "Dashboard",


            });

            builder.HasData(new StaffPermissionType
            {
                Id = 2,
                Name = "SystemWallet",


            });

            builder.HasData(new StaffPermissionType
            {
                Id = 3,
                Name = "AgentManagement",


            });

            builder.HasData(new StaffPermissionType
            {
                Id = 4,
                Name = "SubscriberManagement",


            });

            builder.HasData(new StaffPermissionType
            {
                Id = 5,
                Name = "CustomerService",


            });

            builder.HasData(new StaffPermissionType
            {
                Id = 6,
                Name = "Reports",


            });

            builder.HasData(new StaffPermissionType
            {
                Id = 7,
                Name = "Compliance&RiskManagement",


            });

            builder.HasData(new StaffPermissionType
            {
                Id = 8,
                Name = "SystemAdmin",

            });



        }
    }
}
