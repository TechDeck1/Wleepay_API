using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class IssueTypeConfiguration : IEntityTypeConfiguration<IssueType>
    {
        public void Configure(EntityTypeBuilder<IssueType> builder)
        {
            builder
               .ToTable("IssueType");

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
              .Property(u => u.Description)
              .HasMaxLength(100);

            builder
            .Property(u => u.IsDeleted);


            builder.HasData(new IssueType
            {
                Id = 1,
                Type = "Signup/KYC",
                Description= "Signup/KYC",
                CreatedDate = new DateTime()

            });

            builder.HasData(new IssueType
            {
                Id = 2,
                Type = "Agent Commission",
                Description= "Agent Commission",
                CreatedDate = new DateTime()

            });

            builder.HasData(new IssueType
            {
                Id = 3,
                Type = "App Not Working",
                Description= "App Not Working",
                CreatedDate = new DateTime()

            });

            builder.HasData(new IssueType
            {
                Id = 4,
                Type = "Topup",
                Description="Topup",
                CreatedDate = new DateTime()

            });

            builder.HasData(new IssueType
            {
                Id = 5,
                Type = "Cashout",
                Description = "Cashout",
                CreatedDate = new DateTime()

            });

            builder.HasData(new IssueType
            {
                Id = 6,
                Type = "Payment",
                Description = "Payment",
                CreatedDate = new DateTime()

            });

            builder.HasData(new IssueType
            {
                Id = 7,
                Type = "Donation",
                Description = "Donation",
                CreatedDate = new DateTime()

            });

            builder.HasData(new IssueType
            {
                Id = 8,
                Type = "Reversal",
                Description = "Reversal",
                CreatedDate = new DateTime()

            });

            builder.HasData(new IssueType
            {
                Id = 9,
                Type = "Bill Payment",
                Description = "Bill Payment",
                CreatedDate = new DateTime()

            });
            builder.HasData(new IssueType
            {
                Id = 10,
                Type = "Phone Topup",
                Description= "Phone Topup",
                CreatedDate = new DateTime()

            });

            builder.HasData(new IssueType
            {
                Id = 11,
                Type = "Crypto Wallet",
                Description = "Crypto Wallet",
                CreatedDate = new DateTime()

            });

            builder.HasData(new IssueType
            {
                Id = 12,
                Type = "Agent Wallet",
                Description = "Agent Wallet",
                CreatedDate = new DateTime()

            });

            builder.HasData(new IssueType
            {
                Id = 13,
                Type = "Payment reversal",
                Description = "Payment reversal",
                CreatedDate = new DateTime()

            });
            builder.HasData(new IssueType
            {
                Id = 14,
                Type = "Lost/Stolen Pin",
                Description = "Lost/Stolen Pin",
                CreatedDate = new DateTime()

            });

            builder.HasData(new IssueType
            {
                Id = 15,
                Type = "Identity Theft",
                Description = "Identity Theft",
                CreatedDate = new DateTime()

            });

            builder.HasData(new IssueType
            {
                Id = 16,
                Type = "Wallet Fraud/Theft",
                Description = "Wallet Fraud/Theft",
                CreatedDate = new DateTime()

            });

            builder.HasData(new IssueType
            {
                Id = 17,
                Type = "Report System Outage",
                Description = "Report System Outage",
                CreatedDate = new DateTime()

            });

            builder.HasData(new IssueType
            {
                Id = 18,
                Type = "Other issue (describe)",
                Description = "Report System Outage",
                CreatedDate = new DateTime()

            });
        }
    }
}
