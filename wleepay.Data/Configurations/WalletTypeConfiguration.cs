using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class WalletTypeConfiguration : IEntityTypeConfiguration<WalletType>
    {
        public void Configure(EntityTypeBuilder<WalletType> builder)
        {
            builder
                .ToTable("WalletType");

            builder
                .HasKey(k => k.Id);

            builder
                .Property( k=> k.Id)
                .UseIdentityColumn();

            builder
                .Property(k => k.Name)
                .IsRequired();

            builder.HasData(new WalletType
            {
                Id = 1,
                Name = "Debit float N",
                Description = "System Account"
            });

            builder.HasData(new WalletType
            {
                Id = 2,
                Name = "Organization P",
                Description = "System Account"
            });

            builder.HasData(new WalletType
            {
                Id = 3,
                Name = "Wlee Merchant Fees",
                Description = "System Account"
            });

            builder.HasData(new WalletType
            {
                Id = 4,
                Name = "WleeTax",
                Description = "System Account"
            });

            builder.HasData(new WalletType
            {
                Id = 5,
                Name = "WleePay Agent Comission Account",
                Description = "User Account"
            });

            builder.HasData(new WalletType
            {
                Id = 6,
                Name = "WleePay Agent Account",
                Description = "User Account"
            });

            builder.HasData(new WalletType
            {
                Id = 7,
                Name = "WleePay Member Account",
                Description = "User Account"
            });

            builder.HasData(new WalletType
            {
                Id = 8,
                Name = "WleePay Merchant Account",
                Description = "User Account"
            });

            builder.HasData(new WalletType
            {
                Id = 9,
                Name = "System Wallet",
                Description = "System Account"
            });
        }
    }
}
