using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class CreditCardTransactionConfiguration : IEntityTypeConfiguration<CreditCardTransactions>
    {
        public void Configure(EntityTypeBuilder<CreditCardTransactions> builder)
        {

            builder
               .ToTable("CreditCardTransactions");

            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .UseIdentityColumn();


            builder
               .Property(u => u.StripeTransactionId);

            builder
              .Property(u => u.Amount);

            builder.Property(u => u.PaymentMethod);


            builder
             .Property(u => u.Status);

            builder
              .Property(u => u.BalanceTransactionId);


            builder
             .Property(u => u.ExtraInfo);


            builder
                .Property(u => u.CreatedDate);


        }
    }
}
