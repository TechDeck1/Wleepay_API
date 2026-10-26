using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class OperatorConfiguration : IEntityTypeConfiguration<Operator>
    {
        public void Configure(EntityTypeBuilder<Operator> builder)
        {
            builder.ToTable("Operators");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.SochitelId)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(o => o.SochitelId)
                   .IsUnique();

            builder.Property(o => o.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(o => o.BrandId)
                   .HasMaxLength(50);

            builder.Property(o => o.ProductTypeId)
                   .HasMaxLength(20);

            builder.Property(o => o.ProductTypeName)
                   .HasMaxLength(100);

            var stringListComparer = new ValueComparer<List<string>>(
                (a, b) => (a ?? new List<string>()).SequenceEqual(b ?? new List<string>()),
                v => (v ?? new List<string>()).Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
                v => v == null ? new List<string>() : v.ToList());

            builder.Property(o => o.ProductTypes)
                   .HasConversion(
                       v => JsonConvert.SerializeObject(v),
                       v => JsonConvert.DeserializeObject<List<string>>(v))
                   .Metadata.SetValueComparer(stringListComparer);

            builder.Property(o => o.ProductCategories)
                   .HasConversion(
                       v => JsonConvert.SerializeObject(v),
                       v => JsonConvert.DeserializeObject<List<string>>(v))
                   .Metadata.SetValueComparer(stringListComparer);

            builder.Property(o => o.Locations)
                   .HasConversion(
                       v => JsonConvert.SerializeObject(v),
                       v => JsonConvert.DeserializeObject<List<string>>(v))
                   .Metadata.SetValueComparer(stringListComparer);

            builder.Property(o => o.Prefixes)
                   .HasConversion(
                       v => JsonConvert.SerializeObject(v),
                       v => JsonConvert.DeserializeObject<List<string>>(v))
                   .Metadata.SetValueComparer(stringListComparer);

            builder.Property(o => o.Currency)
                   .HasMaxLength(10);

            builder.Property(o => o.CountryId)
                   .HasMaxLength(10);

            builder.Property(o => o.CountryName)
                   .HasMaxLength(100);

            builder.Property(o => o.CountryPrefixes)
                   .HasConversion(
                       v => JsonConvert.SerializeObject(v),
                       v => JsonConvert.DeserializeObject<List<string>>(v))
                   .Metadata.SetValueComparer(stringListComparer);

            builder.Property(o => o.OperatorPrefixes)
                   .HasConversion(
                       v => JsonConvert.SerializeObject(v),
                       v => JsonConvert.DeserializeObject<List<string>>(v))
                   .Metadata.SetValueComparer(stringListComparer);

            builder.Property(o => o.IsDeleted)
                   .HasDefaultValue(false);

            builder.Property(o => o.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}