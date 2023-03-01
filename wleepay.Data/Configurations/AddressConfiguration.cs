using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder
                .ToTable("Address");

            builder
                .HasKey(k => k.Id);

            builder
                .Property(k => k.Id)
                .UseIdentityColumn();

            builder
                .Property(k => k.UserId)
                .IsRequired()
                .HasMaxLength(450);

            builder
                .Property(k => k.AddressLine1);

            builder
                .Property(k => k.AddressLine2);

            builder
                .Property(k => k.Street)
                .HasMaxLength(50);

            builder
                .Property(k => k.City)
                .HasMaxLength(50);

            builder
                .Property(k => k.State)
                .HasMaxLength(50);

            builder
                 .Property(k => k.ZipCode)
                 .HasMaxLength(10);

            builder
                .Property(k => k.Latitude)
                .HasMaxLength(10);

            builder
                .Property(k => k.Longitude)
                .HasMaxLength(10);

            builder
                .HasOne(k => k.ApplicationUser)
                .WithOne(k => k.Address)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey<Address>(k => k.UserId);
        }
    }
}
