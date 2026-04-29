using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using wleepay.Core.Models;

public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
{
    public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    {
        builder.ToTable("UserTransactions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount)
               .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Street).HasMaxLength(200);  
        builder.Property(p => p.City).HasMaxLength(100);
        builder.Property(p => p.ZipCode).HasMaxLength(20);

        builder.HasOne(p => p.User)
               .WithMany() 
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.ClientNoAction);

        builder.HasOne(p => p.Currency)
               .WithMany()
               .HasForeignKey(p => p.CurrencyId)
               .OnDelete(DeleteBehavior.ClientNoAction);
    }
}
