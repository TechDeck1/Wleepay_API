using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;


namespace wleepay.Data.Configurations
{
    public class SystemStatusConfiguration : IEntityTypeConfiguration<SystemStatus>
    {
        public void Configure(EntityTypeBuilder<SystemStatus> builder)
        {
            builder.ToTable("SystemStatus");

            builder
               .HasKey(k => k.Id);

            builder
                .Property(k => k.Id)
                .UseIdentityColumn();

            builder
                .Property(k => k.Reason);

            builder
                .Property(k => k.Description);

            builder
                .Property(k => k.Status);

            builder
               .Property(k => k.CreatedDate);
           
        }
    }
}
