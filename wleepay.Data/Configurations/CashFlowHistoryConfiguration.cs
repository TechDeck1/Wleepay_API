using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;


namespace wleepay.Data.Configurations
{
    public class CashFlowHistoryConfiguration : IEntityTypeConfiguration<CashFlowHistory>
    {
        public void Configure(EntityTypeBuilder<CashFlowHistory> builder)
        {
            builder
                .ToTable("CashFlowHistory");

            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .UseIdentityColumn();

            builder
                .Property(c => c.TotalAmount)
                .IsRequired();

            builder
               .Property(c => c.WalletId);

            builder
               .Property(c => c.DepositAmount);

            
            builder
            .Property(c => c.WithdrawAmount);

            builder.Property(c => c.PreviousAmount);


            builder
            .Property(c => c.CurrencyId);


            builder
               .Property(c => c.CreatedDate);



            builder
               .HasOne(k => k.toWallet)
               .WithMany(k => k.CashFlowHistory)
               .OnDelete(DeleteBehavior.ClientNoAction)
               .HasForeignKey(k => k.WalletId);


            builder.HasData(new CashFlowHistory
            {
                Id = 1,
                TotalAmount = 0,
                WalletId = 1,
                DepositAmount = 0,
                WithdrawAmount = 0,
                CreatedDate = new DateTime()

            });
        }
    }
}
