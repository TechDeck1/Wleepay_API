using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class StaffPermissionConfiguration : IEntityTypeConfiguration<StaffPermissions>
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


            builder.Property(u => u.Name);


            builder.HasData(new StaffPermissions
            {
                Id = 1,
                Name = "Dashboard",


            });

            builder.HasData(new StaffPermissions
            {
                Id = 2,
                Name = "SystemWallet",


            });

            builder.HasData(new StaffPermissions
            {
                Id = 3,
                Name = "AgentManagement",


            });

            builder.HasData(new StaffPermissions
            {
                Id = 4,
                Name = "SubscriberManagement",


            });

            builder.HasData(new StaffPermissions
            {
                Id = 5,
                Name = "CustomerService",


            });

            builder.HasData(new StaffPermissions
            {
                Id = 6,
                Name = "Reports",


            });

            builder.HasData(new StaffPermissions
            {
                Id = 7,
                Name = "Compliance&RiskManagement",


            });

            builder.HasData(new StaffPermissions
            {
                Id = 8,
                Name = "SystemAdmin",

            });



        }
    }
}
