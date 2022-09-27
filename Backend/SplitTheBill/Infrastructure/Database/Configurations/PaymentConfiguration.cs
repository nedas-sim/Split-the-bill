using Domain.Database;
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
        paymentBuilder.HasKey(x => x.Id);
        
        ConfigurationHelper.ConfigureIdForEntity<Payment, PaymentId>(paymentBuilder);
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