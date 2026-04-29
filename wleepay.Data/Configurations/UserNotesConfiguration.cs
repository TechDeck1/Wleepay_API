using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class UserNotesConfiguration : IEntityTypeConfiguration<UserNotes>
    {
        public void Configure(EntityTypeBuilder<UserNotes> builder)
        {
            builder
                   .ToTable("UserNotes");

            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .UseIdentityColumn();

            builder
               .Property(u => u.CreatedDate);

            builder
             .Property(u => u.UpdatedDate);

            builder
              .Property(u => u.Note)
              .HasMaxLength(500)
              .IsRequired();

            builder
             .Property(u => u.AddedBy);

            builder
              .Property(u => u.IsDeleted);

            builder
               .HasOne(u => u.ApplicationUser)
               .WithMany(u => u.UserNotes)
               .OnDelete(DeleteBehavior.ClientNoAction)
               .HasForeignKey(u => u.userId);

        }
    }
}
