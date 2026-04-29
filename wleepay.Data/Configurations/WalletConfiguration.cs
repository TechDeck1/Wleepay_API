using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder
                .ToTable("Wallet");

            builder
                .HasKey(k => k.Id);

            builder
                .Property( k=> k.Id)
                .UseIdentityColumn();

            builder
                .Property(k => k.UserId)
                .IsRequired();

            builder
                .Property(k => k.CurrencyId)
                .IsRequired();

            builder
                .Property(k => k.Type)
                .IsRequired();

            builder
                .Property(k => k.Balance)
                .IsRequired();

            builder
                .Property(k => k.CreatedDate)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder
                .Property(k => k.UpdatedDate)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder
                .HasOne(k => k.ApplicationUser)
                .WithMany(k => k.Wallets)
                .HasForeignKey(k => k.UserId);

            builder
                .HasOne(k => k.Currency)
                .WithMany(k => k.Wallets)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(k => k.CurrencyId);

            builder
                .HasOne(k => k.WalletType)
                .WithMany(k => k.Wallets)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(k => k.Type);

            builder.HasData(new Wallet
            {
                Id = 1,
                UserId = "07ac193d-26e7-4039-9440-fbb53988b878",
                CurrencyId = Core.Enums.Currencies.LRD.GetHashCode(),
                Type = Core.Enums.WalletTypes.DebitFloatN.GetHashCode(),
                Balance = 0,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            builder.HasData(new Wallet
            {
                Id = 2,
                UserId = "07ac193d-26e7-4039-9440-fbb53988b878",
                CurrencyId = Core.Enums.Currencies.USD.GetHashCode(),
                Type = Core.Enums.WalletTypes.DebitFloatN.GetHashCode(),
                Balance = 0,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            builder.HasData(new Wallet
            {
                Id = 3,
                UserId = "07ac193d-26e7-4039-9440-fbb53988b878",
                CurrencyId = Core.Enums.Currencies.LRD.GetHashCode(),
                Type = Core.Enums.WalletTypes.OrgP.GetHashCode(),
                Balance = 0,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            builder.HasData(new Wallet
            {
                Id = 4,
                UserId = "07ac193d-26e7-4039-9440-fbb53988b878",
                CurrencyId = Core.Enums.Currencies.USD.GetHashCode(),
                Type = Core.Enums.WalletTypes.OrgP.GetHashCode(),
                Balance = 0,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            builder.HasData(new Wallet
            {
                Id = 5,
                UserId = "07ac193d-26e7-4039-9440-fbb53988b878",
                CurrencyId = Core.Enums.Currencies.LRD.GetHashCode(),
                Type = Core.Enums.WalletTypes.WleeTax.GetHashCode(),
                Balance = 0,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            builder.HasData(new Wallet
            {
                Id = 6,
                UserId = "07ac193d-26e7-4039-9440-fbb53988b878",
                CurrencyId = Core.Enums.Currencies.USD.GetHashCode(),
                Type = Core.Enums.WalletTypes.WleeTax.GetHashCode(),
                Balance = 0,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            builder.HasData(new Wallet
            {
                Id = 7,
                UserId = "07ac193d-26e7-4039-9440-fbb53988b878",
                CurrencyId = Core.Enums.Currencies.LRD.GetHashCode(),
                Type = Core.Enums.WalletTypes.SystemWallet.GetHashCode(),
                Balance = 0,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            builder.HasData(new Wallet
            {
                Id = 8,
                UserId = "07ac193d-26e7-4039-9440-fbb53988b878",
                CurrencyId = Core.Enums.Currencies.USD.GetHashCode(),
                Type = Core.Enums.WalletTypes.SystemWallet.GetHashCode(),
                Balance = 0,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });
        }
    }
}
