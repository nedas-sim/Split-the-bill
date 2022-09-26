using Domain.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configurations;

public class UserConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        var userModelBuilder = modelBuilder.Entity<User>();

        userModelBuilder.HasKey(x => x.Id);
        userModelBuilder.Property(x => x.Id)
                        .HasConversion(x => x.Id, id => new UserId { Id = id })
                        .HasColumnName(nameof(User.Id))
                        .ValueGeneratedOnAdd()
                        .IsRequired()
                        ;
    }
}