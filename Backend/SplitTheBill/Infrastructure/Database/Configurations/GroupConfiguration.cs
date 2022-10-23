using Domain.Database;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Views;

namespace Infrastructure.Database.Configurations;

public static class GroupConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<UserGroup> userGroupBuilder = modelBuilder.Entity<UserGroup>();

        ConfigureUserGroups(userGroupBuilder);
        ConfigureViews(modelBuilder);
    }

    private static void ConfigureUserGroups(EntityTypeBuilder<UserGroup> builder)
    {
        builder.HasKey(x => new { x.UserId, x.GroupId });

        builder.HasOne(ug => ug.User)
               .WithMany(u => u.UserGroups)
               .HasForeignKey(ug => ug.UserId);

        builder.HasOne(ug => ug.Group)
               .WithMany(g => g.UserGroups)
               .HasForeignKey(ug => ug.GroupId);
    }

    private static void ConfigureViews(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupMembershipView>(x =>
        {
            x.HasNoKey();
            x.ToView(nameof(GroupMembershipView));
        });
    }
}