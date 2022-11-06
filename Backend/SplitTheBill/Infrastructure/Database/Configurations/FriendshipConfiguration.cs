using Domain.Database;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public static class FriendshipConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<UserFriendship> userFriendshipBuilder = modelBuilder.Entity<UserFriendship>();

        ConfigureUserFriendships(userFriendshipBuilder);
        ConfigureViews(modelBuilder);
    }

    private static void ConfigureUserFriendships(EntityTypeBuilder<UserFriendship> builder)
    {
        builder.HasKey(x => new { x.RequestSenderId, x.RequestReceiverId });

        builder.HasOne(uf => uf.RequestSender)
               .WithMany()
               .HasForeignKey(uf => uf.RequestSenderId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(uf => uf.RequestReceiver)
               .WithMany()
               .HasForeignKey(uf => uf.RequestReceiverId)
               .OnDelete(DeleteBehavior.NoAction);
    }

    private static void ConfigureViews(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcceptedFriendshipView>(x =>
        {
            x.HasNoKey();
            x.ToView(nameof(AcceptedFriendshipView));
        });

        modelBuilder.Entity<PendingFriendshipView>(x =>
        {
            x.HasNoKey();
            x.ToView(nameof(PendingFriendshipView));
        });
    }
}