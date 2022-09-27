using Domain.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configurations;

public class UserConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        var userModelBuilder = modelBuilder.Entity<User>();

        userModelBuilder.HasKey(x => x.Id);

        ConfigurationHelper.ConfigureIdForEntity<User, UserId>(userModelBuilder);
    }
}