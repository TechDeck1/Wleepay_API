using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class SystemActivitiesConfiguration : IEntityTypeConfiguration<SystemActivityLog>
    {
        public void Configure(EntityTypeBuilder<SystemActivityLog> builder)
        {
            builder
                .ToTable("SystemActivities");

            builder
                .HasKey(k => k.Id);

            builder
                .Property(k => k.Id)
                .UseIdentityColumn();

            builder
                .Property(k => k.UserId)
                .IsRequired();

            builder
                .Property(k => k.Email);

            builder
                .Property(k => k.IpAddress);
            builder
               .Property(k => k.CreatedDate);
            builder
               .Property(k => k.Description);
            builder
              .Property(k => k.Action);


        }
    }
}
