using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class TransferFeeConfiguration : IEntityTypeConfiguration<TransferFee>
    {
        public void Configure(EntityTypeBuilder<TransferFee> builder)
        {
            builder
                .ToTable("TransferFee");

            builder
                .HasKey(t => t.Id);

            builder
                .Property( t=> t.Id)
                .UseIdentityColumn();

            builder
                .Property(t => t.TaxAmount)
                .IsRequired();

            builder
                .Property(t => t.OrganizationFee)
                .IsRequired();

            builder
                .Property(t => t.AgentCommission)
                .IsRequired();

            builder
                .Property(t => t.Description);

            builder.HasData(new TransferFee
            {
                Id = 1,
                TaxAmount = 0.14,
                OrganizationFee = 1.21,
                AgentCommission = 1.35,
                Description = "CashOut Type transfer fee and taxes"
            });
        }
    }
}
