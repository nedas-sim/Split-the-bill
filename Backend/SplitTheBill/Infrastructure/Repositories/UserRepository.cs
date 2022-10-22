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

    public async Task<bool> EmailExists(string email, CancellationToken cancellationToken = default)
    {
        bool exists =
            await QueryByEmail(email)
                .AnyAsync(cancellationToken);

        return exists;
    }

    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        User? user =
            await QueryByEmail(email)
                .FirstOrDefaultAsync(cancellationToken);

        return user;
    }

    public async Task<bool> UsernameExists(string username, CancellationToken cancellationToken = default)
    {
        bool exists =
            await context.Users
                         .Where(u => u.Username == username)
                         .AnyAsync(cancellationToken);

        return exists;
    }

    #region Private Methods
    private IQueryable<User> QueryByEmail(string email)
    {
        IQueryable<User> queryByEmail = 
            context.Users
                   .Where(u => u.Email == email);

        return queryByEmail;
    }
    #endregion
}