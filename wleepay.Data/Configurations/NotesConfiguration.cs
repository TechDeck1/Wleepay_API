using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class NotesConfiguration : IEntityTypeConfiguration<Notes>
    {
        public void Configure(EntityTypeBuilder<Notes> builder)
        {

            builder
               .ToTable("Notes");

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
              .HasMaxLength(250)
              .IsRequired();


            builder
                .Property(u => u.CustomerTicketId)
                .IsRequired();

            builder
              .Property(u => u.IsDeleted);


            builder
                 .HasOne(u => u.CustomerTickets)
                 .WithMany(u => u.Notes)
                 .OnDelete(DeleteBehavior.ClientNoAction)
                 .HasForeignKey(u => u.CustomerTicketId);

        }
    }
}
