using Domain.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configurations;

public class UserConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        var userModelBuilder = modelBuilder.Entity<User>();

        ConfigurationHelper.ConfigureIdForEntity<User, UserId>(userModelBuilder);
    }
}