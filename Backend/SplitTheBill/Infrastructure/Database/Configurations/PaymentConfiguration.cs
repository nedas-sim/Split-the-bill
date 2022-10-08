using Domain.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public static class PaymentConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<UserPayment> userPaymentBuilder = modelBuilder.Entity<UserPayment>();

        ConfigureUserPayments(userPaymentBuilder);
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