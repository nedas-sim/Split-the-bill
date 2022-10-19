using Domain.Database;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configurations;

public static class GroupConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<UserGroup> userGroupBuilder = modelBuilder.Entity<UserGroup>();

        ConfigureUserGroups(userGroupBuilder);
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
}