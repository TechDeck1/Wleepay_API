using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;


namespace wleepay.Data.Configurations
{
    public class UserStaffRoleConfiguration : IEntityTypeConfiguration<UsersStaffRole>
    {
        public UserStaffRoleConfiguration() { 
        }

        public void Configure(EntityTypeBuilder<UsersStaffRole> builder)
        {
            builder
               .ToTable("UsersStaffRole");

            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .UseIdentityColumn();

           
            builder
                .Property(u => u.UserId)
                .IsRequired();
          

            builder
               .Property(u => u.StaffRoleId)
               .IsRequired();


            builder
                 .HasOne(u => u.StaffRoles)
                 .WithMany(u => u.UsersStaffRole)
                 .OnDelete(DeleteBehavior.ClientNoAction)
                 .HasForeignKey(u => u.StaffRoleId);

            builder
                .HasOne(u => u.ApplicationUser)
                .WithMany(u => u.UsersStaffRole)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(u => u.UserId);

        }
    }
}
