using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .IsRequired();

            builder
                .HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .IsRequired();

            builder.HasData(new ApplicationUserRole
            {
                RoleId = "dbcebf6c-174e-407f-b170-136cbc68813e",
                UserId = "07ac193d-26e7-4039-9440-fbb53988b878"
            });
        }
    }
}
