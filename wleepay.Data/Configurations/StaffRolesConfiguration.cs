using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class StaffRolesConfiguration :IEntityTypeConfiguration<StaffRoles>
    {
        public void Configure(EntityTypeBuilder<StaffRoles> builder)
        {
            builder
               .ToTable("StaffRoles");

            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .UseIdentityColumn();


            builder.Property(u => u.Name);


            builder.HasData(new StaffRoles
            {
                Id = 1,
                Name = "Marketing",
             

            });

            builder.HasData(new StaffRoles
            {
                Id = 2,
                Name = "HR",
              

            });

            builder.HasData(new StaffRoles
            {
                Id = 3,
                Name = "CustomerService",
               

            });

            builder.HasData(new StaffRoles
            {
                Id = 4,
                Name = "IT",
              

            });

            builder.HasData(new StaffRoles
            {
                Id = 5,
                Name = "SystemWallet",
              

            });

            builder.HasData(new StaffRoles
            {
                Id = 6,
                Name = "Finance",
             

            });

            builder.HasData(new StaffRoles
            {
                Id = 7,
                Name = "SystemAdmin",
               

            });

           
        }
    }
}
