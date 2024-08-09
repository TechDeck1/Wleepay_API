using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class TransferTypeConfiguration : IEntityTypeConfiguration<TransferType>
    {
        public void Configure(EntityTypeBuilder<TransferType> builder)
        {
            builder
                .ToTable("TransferType");

            builder
                .HasKey(k => k.Id);

            builder
                .Property( k=> k.Id)
                .UseIdentityColumn();

            builder
                .Property(k => k.Name)
                .IsRequired();

            builder
                .Property(k => k.FromWalletType);

            builder
                .Property(k => k.ToWalletType);

            builder
                .Property(k => k.Description);

            builder
                .Property(k => k.TransferFee);

            builder
                .HasOne(k => k.TransferFeeDetail)
                .WithMany(k => k.TransferTypes)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(k => k.TransferFee);

            builder
                .HasOne(k => k.FromWalletTypeDetail)
                .WithMany(k => k.TransferTypes)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(k => k.FromWalletType);

            builder
                .HasOne(k => k.ToWalletTypeDetail)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(k => k.ToWalletType);

            builder.HasData(new TransferType
            {
                Id = 1,
                Name = "Cash Out",
                FromWalletType = 7,
                ToWalletType = 6,
                Description = "Transfer from member a/c to agent a/c",
                TransferFee = 1
            });

            builder.HasData(new TransferType
            {
                Id = 2,
                Name = "Generated Agent Commission",
                FromWalletType = 7,
                ToWalletType = 5,
                Description = "Transfer from memeber to agent commission account",
                TransferFee = null
            });

            builder.HasData(new TransferType
            {
                Id = 3,
                Name = "Wlee Tax Transfer",
                FromWalletType = 7,
                ToWalletType = 4,
                Description = "Transfer from member to wlee tax account",
                TransferFee = null
            });

            builder.HasData(new TransferType
            {
                Id = 4,
                Name = "Cash Out Standard Fee Split",
                FromWalletType = 7,
                ToWalletType = 2,
                Description = "Transfer from member to organization account p",
                TransferFee = null
            });

            builder.HasData(new TransferType
            {
                Id = 5,
                Name = "Wlee Transfer",
                FromWalletType = 7,
                ToWalletType = 7,
                Description = "Transfer from wleepay member acount to other member account",
                TransferFee = null
            });

            builder.HasData(new TransferType
            {
                Id = 6,
                Name = "Top Up",
                FromWalletType = 6,
                ToWalletType = 7,
                Description = "TopUp member wallet from agent a/c",
                TransferFee = null
            });

            builder.HasData(new TransferType
            {
                Id = 7,
                Name = "Fill Agent",
                FromWalletType = 1,
                ToWalletType = 6,
                Description = "Fill Agent a/c from system float n a/c",
                TransferFee = null
            });

            
            builder.HasData(new TransferType
            {
                Id = 8,
                Name = "Cashout Agent Commission",
                FromWalletType = 5,
                ToWalletType = 1,
                Description = "Transfer from Agent Commission a/c  to system float n a/c",
                TransferFee = null
            });

            builder.HasData(new TransferType
            {
                Id = 9,
                Name = "Top Up System Wallet",
                FromWalletType = 1,
                ToWalletType = 9,
                Description = "Transfer from system float n a/c to sytem wallet",
                TransferFee = null
            });
            builder.HasData(new TransferType
            {
                Id = 10,
                Name = "Mobile Recharge Transfer",
                FromWalletType = 7,
                ToWalletType = 1,
                Description = "Transfer from member a/c to system float a/c",
                TransferFee = null
            });
            builder.HasData(new TransferType
            {
                Id = 11,
                Name = "Bill Payment",
                FromWalletType = 7,
                ToWalletType = 1,
                Description = "Transfer from member a/c to system float a/c",
                TransferFee = null
            });
        }
    }
}
