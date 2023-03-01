using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class TransactionStatusConfiguration : IEntityTypeConfiguration<TransactionStatus>
    {
        public void Configure(EntityTypeBuilder<TransactionStatus> builder)
        {
            builder
                .ToTable("TransactionStatus");

            builder
                .HasKey(k => k.Id);

            builder
                .Property( k=> k.Id)
                .UseIdentityColumn();

            builder
                .Property(k => k.Name)
                .IsRequired();

            builder.HasData(new TransactionStatus
            {
                Id = 1,
                Name = "Pending"
            });

            builder.HasData(new TransactionStatus
            {
                Id = 2,
                Name = "Rejected"
            });

            builder.HasData(new TransactionStatus
            {
                Id = 3,
                Name = "Approved"
            });

            builder.HasData(new TransactionStatus
            {
                Id = 4,
                Name = "Failed"
            });

            builder.HasData(new TransactionStatus
            {
                Id = 5,
                Name = "Cancel"
            });
        }
    }
}
