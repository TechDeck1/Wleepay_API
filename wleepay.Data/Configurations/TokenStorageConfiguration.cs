using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class TokenStorageConfiguration : IEntityTypeConfiguration<TokenStorage>
    {
            public void Configure(EntityTypeBuilder<TokenStorage> builder)
            {
                builder
                    .ToTable("TokenStorage");

                builder
                    .HasKey(k => k.Id);

                builder
                    .Property(k => k.Id)
                    .UseIdentityColumn();

                builder
                    .Property(k => k.Token)
                    .IsRequired();

                builder
                    .Property(k => k.UserId);

                builder
                    .Property(k => k.ExpirationTime);

           
            }
        
    }
}
