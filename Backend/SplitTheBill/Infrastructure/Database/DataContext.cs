using Domain.Database;
using Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public sealed class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options)
		: base(options) { }

	public DbSet<User> Users { get; set; }
	public DbSet<Payment> Payments { get; set; }
	public DbSet<UserPayment> UserPayments { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		PaymentConfiguration.Configure(modelBuilder);
	}
}