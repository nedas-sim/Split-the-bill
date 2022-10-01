using Domain.Database;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public static class PaymentConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<Payment> paymentBuilder = modelBuilder.Entity<Payment>();
        EntityTypeBuilder<UserPayment> userPaymentBuilder = modelBuilder.Entity<UserPayment>();

        ConfigurePayments(paymentBuilder);
        ConfigureUserPayments(userPaymentBuilder);
    }

    private static void ConfigurePayments(EntityTypeBuilder<Payment> paymentBuilder)
    {
        ConfigurationHelper.ConfigureIdForEntity<Payment, PaymentId>(paymentBuilder);

        paymentBuilder.OwnsOne(p => p.Amount)
                      .Property(p => p.Value).HasColumnName(nameof(Payment.Amount));

        paymentBuilder.OwnsOne(p => p.Amount)
                      .Property(p => p.Currency).HasColumnName(nameof(Amount.Currency));
    }

    private static void ConfigureUserPayments(EntityTypeBuilder<UserPayment> userPaymentBuilder)
    {
        userPaymentBuilder.HasKey(x => new { x.UserId, x.PaymentId });

        userPaymentBuilder.HasOne(up => up.User)
                          .WithMany(u => u.UserPayments)
                          .HasForeignKey(up => up.UserId);

        userPaymentBuilder.HasOne(up => up.Payment)
                          .WithMany(p => p.UserPayments)
                          .HasForeignKey(up => up.PaymentId);
    }
}