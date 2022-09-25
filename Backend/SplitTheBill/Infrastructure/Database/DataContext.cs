using Domain.Database;
using Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public sealed class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options)
		: base(options) { }

	public DbSet<User> Users { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		UserConfiguration.Configure(modelBuilder);
	}
}