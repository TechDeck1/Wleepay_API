using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
                .ToTable("Transaction");

            builder
                .HasKey(k => k.Id);

            builder
                .Property( k=> k.Id)
                .UseIdentityColumn();

            builder
                .Property(k => k.ToWalletId)
                .IsRequired();

            builder
                .Property(k => k.FromWalletId)
                .IsRequired();

            builder
                .Property(k => k.Amount)
                .IsRequired();

            builder
                .Property(k => k.Description);

            builder
                .Property(k => k.CreatedDate)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder
                .Property(k => k.ParentId);

            builder
                .HasOne(k => k.FromWallet)
                .WithMany(k => k.TransactionFrom)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(k => k.FromWalletId);

            builder
                .HasOne(k => k.ToWallet)
                .WithMany(k => k.TransactionTo)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(k => k.ToWalletId);

            builder
                .HasOne(k => k.TransferType)
                .WithMany(k => k.Transactions)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(k => k.TransferTypeId);

            builder
                .HasOne(k => k.TransactionStatus)
                .WithMany(k => k.Transactions)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(k => k.Status);
        }
    }
}
