using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
    {
        public void Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder
                .ToTable("UserDetail");

            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .UseIdentityColumn();

            builder
                .Property(u => u.UserId)
                .HasMaxLength(450)
                .IsRequired();

            builder
                .Property(u => u.FirstName)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(u => u.LastName)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(u => u.Gender)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(u => u.PhoneNumber)
                .HasMaxLength(13)
                .IsRequired();

            builder
                .Property(u => u.DOB);

            builder
                .Property(u => u.KYCStatusId)
                .IsRequired();

            builder
                .Property(u => u.Image);

            builder
                .Property(u => u.CountyId)
                .IsRequired();

            builder
                .Property(u => u.KYCLevel);

            builder
                .Property(u => u.IsLoggedIn)
                .IsRequired();

            builder
                .Property(u => u.VerificationCode);

            builder.Property(u => u.VerificationExpiry);

            builder.Property(u => u.SecurityQuestionId);

            builder.Property(u => u.SecurityAnswerText).HasMaxLength(50);

            builder.Property(u => u.Status);


            builder
                .HasOne(u => u.ApplicationUser)
                .WithOne(u => u.UserDetail)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey<UserDetail>(u => u.UserId);

            builder
                 .HasOne(u => u.KYCStatus)
                 .WithMany(u => u.UserDetails)
                 .OnDelete(DeleteBehavior.ClientNoAction)
                 .HasForeignKey(u => u.KYCStatusId);

            builder
                 .HasOne(u => u.County)
                 .WithMany(u => u.UserDetails)
                 .OnDelete(DeleteBehavior.ClientNoAction)
                 .HasForeignKey(u => u.CountyId);

            builder
                 .HasOne(u => u.KYCLevelDetail)
                 .WithMany(u => u.UserDetails)
                 .OnDelete(DeleteBehavior.ClientNoAction)
                 .HasForeignKey(u => u.KYCLevel);

            builder
                 .HasOne(u => u.SecurityQuestions)
                 .WithMany(u => u.UserDetails)
                 .OnDelete(DeleteBehavior.ClientNoAction)
                 .HasForeignKey(u => u.SecurityQuestionId);

            builder.HasData(new UserDetail
            {
                Id = 1,
                UserId = "07ac193d-26e7-4039-9440-fbb53988b878",
                KYCStatusId = 2,
                CountyId = 2,
                FirstName = "Admin",
                LastName = "Admin",
                Gender = "Male",
                IsLoggedIn = true,
                DOB = DateTime.Parse("1985-09-20 05:01:50.563"),
                PhoneNumber = "+919845654543",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                AccountNumber = "50285368"
            });
        }
    }

}
