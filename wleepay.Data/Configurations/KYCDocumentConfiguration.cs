using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class KYCDocumentConfiguration : IEntityTypeConfiguration<KYCdocuments>
    {
        public void Configure(EntityTypeBuilder<KYCdocuments> builder)
        {
            builder
                .ToTable("KYCDocuments");

            builder
                .HasKey(k => k.Id);

            builder
                .Property( k=> k.Id)
                .UseIdentityColumn();

            builder
                .Property(k => k.DocumentImage);

            builder
                .Property(k => k.ExtractInfo)
                .HasMaxLength(50);

            builder
                .Property(k => k.UserId)
                .IsRequired();

            builder
                .Property(k => k.Type)
                .IsRequired();

            builder
                .Property(k => k.DocumentNumber)
                .HasMaxLength(20);

            builder
                .Property(k => k.CreatedDate)
                .HasDefaultValue(DateTime.Now);

            builder
                .Property(k => k.UpdatedDate)
                .HasDefaultValue(DateTime.Now);

            builder
                .HasOne(k => k.ApplicationUser)
                .WithMany(k => k.KYCdocuments)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(k => k.UserId);

            builder
                .HasOne(k => k.DocumentType)
                .WithMany(k => k.KYCdocuments)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(k => k.Type);
        }
    }
}
