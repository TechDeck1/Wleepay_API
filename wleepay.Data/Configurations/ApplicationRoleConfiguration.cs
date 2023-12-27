using DevExpress.Utils.About;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Xml.Linq;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(new ApplicationRole
            {
                Id = "88b7d36f-2f30-4682-b801-fbd6b40b2af6",
                Name = "Member",
                NormalizedName="MEMBER",
                ConcurrencyStamp = "0aefcb24-3cff-4c92-9bb6-18fef511f3ed"
            });

            builder.HasData(new ApplicationRole
            {
                Id = "dbcebf6c-174e-407f-b170-136cbc68813e",
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN",
                ConcurrencyStamp = "3f4da7d6-e6ae-493e-a343-07732ad7bc65"
            });

            builder.HasData(new ApplicationRole
            {
                Id = "e4e7188b-6ecb-4278-aeee-17271f20d7ce",
                Name = "Agent",
                NormalizedName = "AGENT",
                ConcurrencyStamp = "64559bb4-bcea-4c00-a7c9-ac0cd7037bfd"
            });

            builder.HasData(new ApplicationRole
            {
                Id = "F58A3546-E959-4B1D-A40C-518EAF9D9788",
                Name = "staff",
                NormalizedName = "STAFF",
                ConcurrencyStamp = "71EACF2C-9A1F-4602-9D47-6DE367F4F0EF"
            });
         
        }
    }
}
