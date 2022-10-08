using Application.Repositories;
using Domain.Database;
using Domain.Responses.Users;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DataContext context) : base(context)
    {
    }

    public async Task<UserResponse?> GetUserResponse(Guid id, CancellationToken cancellationToken = default)
    {
        UserResponse? user =
            await context.Users
                         .Where(u => u.Id == id)
                         .Select(u => new UserResponse(u))
                         .FirstOrDefaultAsync(cancellationToken);

        return user;
    }
}