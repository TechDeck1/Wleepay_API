using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder
                .ToTable("DocumentType");

            builder
                .HasKey(d => d.Id);

            builder
                .Property(d => d.Id)
                .UseIdentityColumn();

            builder
                .Property(d => d.Name)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasData(new DocumentType
            {
                Id = 1,
                Name = "National ID Card"
            });

            builder.HasData(new DocumentType
            {
                Id = 2,
                Name = "Driver’s License"
            });

            builder.HasData(new DocumentType
            {
                Id = 3,
                Name = "Voter Card"
            });
        }
    }
}
