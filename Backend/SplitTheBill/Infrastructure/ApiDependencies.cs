using Application.Repositories;
using Application.Services;
using Application.Users.GetUserById;
using Domain.Common;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ApiDependencies
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection @this, IConfiguration configuration)
    {
        // Add db context:
        string connString = configuration.GetConnectionString(nameof(ConnectionStrings.DefaultConnection));
        @this.AddDbContext<DataContext>(options => options.UseSqlServer(connString));

        AddRepositories(@this);
        AddServices(@this);
        AddMediatR(@this);

        return @this;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IPaymentRepository, PaymentRepository>();
        services.AddTransient<IGroupRepository, GroupRepository>();
        services.AddTransient<IEntryRepository, EntryRepository>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddTransient<IAuthorizeService, AuthorizeService>();
    }

    private static void AddMediatR(IServiceCollection services)
    {
        services.AddMediatR(typeof(GetUserByIdQueryHandler));
    }
}