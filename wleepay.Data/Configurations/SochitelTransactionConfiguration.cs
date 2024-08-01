using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class SochitelTransactionConfiguration : IEntityTypeConfiguration<SochitelTransactions>
    {
        public void Configure(EntityTypeBuilder<SochitelTransactions> builder)
        {

            builder
               .ToTable("SochitelTransactions");

            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .UseIdentityColumn();


            builder
               .Property(u => u.TransactionId);

            builder
              .Property(u => u.TransactionAmount);

            builder .Property(u => u.TransactionType);


            builder
             .Property(u => u.CommissionRate);

            builder
              .Property(u => u.PhoneNumber);
              

            builder
             .Property(u => u.ProductId);


            builder
                .Property(u => u.OperatorId);

            builder
              .Property(u => u.UserReference);

        }
    }
}
