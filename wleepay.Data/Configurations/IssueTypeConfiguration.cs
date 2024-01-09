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
            builder.HasData(new IssueType
            {
                Id = 1,
                Type = "Signup/KYC"

            });

            builder.HasData(new IssueType
            {
                Id = 2,
                Type = "Agent Commission"

            });

            builder.HasData(new IssueType
            {
                Id = 3,
                Type = "App Not Working"

            });

            builder.HasData(new IssueType
            {
                Id = 4,
                Type = "Topup"

            });

            builder.HasData(new IssueType
            {
                Id = 5,
                Type = "Cashout"

            });

            builder.HasData(new IssueType
            {
                Id = 6,
                Type = "Payment"

            });

            builder.HasData(new IssueType
            {
                Id = 7,
                Type = "Donation"

            });

            builder.HasData(new IssueType
            {
                Id = 8,
                Type = "Reversal"

            });

            builder.HasData(new IssueType
            {
                Id = 9,
                Type = "Bill Payment"

            });
            builder.HasData(new IssueType
            {
                Id = 10,
                Type = "Phone Topup"

            });

            builder.HasData(new IssueType
            {
                Id = 11,
                Type = "Crypto Wallet"

            });

            builder.HasData(new IssueType
            {
                Id = 12,
                Type = "Agent Wallet"

            });

            builder.HasData(new IssueType
            {
                Id = 13,
                Type = "Payment reversal"

            });
            builder.HasData(new IssueType
            {
                Id = 14,
                Type = "Lost/Stolen Pin"

            });

            builder.HasData(new IssueType
            {
                Id = 15,
                Type = "Identity Theft"

            });

            builder.HasData(new IssueType
            {
                Id = 16,
                Type = "Wallet Fraud/Theft"

            });

            builder.HasData(new IssueType
            {
                Id = 17,
                Type = "Report System Outage"

            });

            builder.HasData(new IssueType
            {
                Id = 18,
                Type = "Other issue (describe)"

            });
        }
    }
}
