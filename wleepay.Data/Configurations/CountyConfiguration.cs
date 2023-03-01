using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class CountyConfiguration : IEntityTypeConfiguration<County>
    {
        public void Configure(EntityTypeBuilder<County> builder)
        {
            builder
                .ToTable("County");

            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .UseIdentityColumn();

            builder
                .Property(c => c.Name)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasData(new County
            {
                Id = 1,
                Name = "Bomi"
            });
            builder.HasData(new County
            {
                Id = 2,
                Name = "Bong"
            });
            builder.HasData(new County
            {
                Id = 3,
                Name = "Grand Bassa"
            });
            builder.HasData(new County
            {
                Id = 4,
                Name = "Margibi"
            });
            builder.HasData(new County
            {
                Id = 5,
                Name = "Montserrado"
            });
            builder.HasData(new County
            {
                Id = 6,
                Name = "Nimba"
            });
            builder.HasData(new County
            {
                Id = 7,
                Name = "Gbarpolu"
            });
            builder.HasData(new County
            {
                Id = 8,
                Name = "Grant Cape Mount"
            });
            builder.HasData(new County
            {
                Id = 9,
                Name = "Lofa"
            });
            builder.HasData(new County
            {
                Id = 10,
                Name = "Grand Gedeh"
            });
            builder.HasData(new County
            {
                Id = 11,
                Name = "Grand Kru"
            });
            builder.HasData(new County
            {
                Id = 12,
                Name = "Maryland"
            });
            builder.HasData(new County
            {
                Id = 13,
                Name = "River Cess"
            });
            builder.HasData(new County
            {
                Id = 14,
                Name = "River Gee"
            });
            builder.HasData(new County
            {
                Id = 15,
                Name = "Sinoe"
            });
        }
    }
}
