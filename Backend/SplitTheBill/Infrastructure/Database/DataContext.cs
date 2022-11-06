using Domain.Database;
using Domain.Views;
using Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public sealed class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options)
		: base(options) { }

    #region Tables
    public DbSet<User> Users { get; set; }
	public DbSet<Payment> Payments { get; set; }
	public DbSet<UserPayment> UserPayments { get; set; }
	public DbSet<Group> Groups { get; set; }
	public DbSet<UserGroup> UserGroups { get; set; }
	public DbSet<UserFriendship> UserFriendships { get; set; }
    #endregion

    #region Views
	public DbSet<GroupMembershipView> GroupMembershipViews { get; set; }
	public DbSet<AcceptedFriendshipView> AcceptedFriendshipViews { get; set; }
	public DbSet<PendingFriendshipView> PendingFriendshipViews { get; set; }
	#endregion

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		PaymentConfiguration.Configure(modelBuilder);
		GroupConfiguration.Configure(modelBuilder);
		FriendshipConfiguration.Configure(modelBuilder);
	}
}