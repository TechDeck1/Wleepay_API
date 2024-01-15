using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class CustomerTicketsConfiguration: IEntityTypeConfiguration<CustomerTickets>
    {
        public void Configure(EntityTypeBuilder<CustomerTickets> builder)
        {
            builder
                .ToTable("CustomerTickets");

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
              .Property(u => u.PhoneNumber)
              .HasMaxLength(13)
              .IsRequired();

            builder
             .Property(u => u.CustomerContact);

            builder
                .Property(u => u.IssueTypeId)
                .IsRequired();

            builder.Property(u => u.Status);

            builder
                 .HasOne(u => u.IssueType)
                 .WithMany(u => u.CustomerTickets)
                 .OnDelete(DeleteBehavior.ClientNoAction)
                 .HasForeignKey(u => u.IssueTypeId);
          
        }

    }
}
