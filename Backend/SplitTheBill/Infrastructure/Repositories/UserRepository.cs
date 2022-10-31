using Application.Repositories;
using Application.Users.GetUserList;
using Domain.Common;
using Domain.Database;
using Domain.Responses.Users;
using Infrastructure.Database;
using Infrastructure.Extensions;
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
                         .AsNoTracking()
                         .Where(u => u.Id == id)
                         .Select(u => new UserResponse
                         {
                             Id = u.Id,
                             Username = u.Username,
                         })
                         .FirstOrDefaultAsync(cancellationToken);

        return user;
    }

    public async Task<bool> EmailExists(string email, CancellationToken cancellationToken = default)
    {
        bool exists =
            await QueryByEmail(email)
                .AsNoTracking()
                .AnyAsync(cancellationToken);

        return exists;
    }

    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        User? user =
            await QueryByEmail(email)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

        return user;
    }

    public async Task<bool> UsernameExists(string username, CancellationToken cancellationToken = default)
    {
        bool exists =
            await context.Users
                         .AsNoTracking()
                         .Where(u => u.Username == username)
                         .AnyAsync(cancellationToken);

        return exists;
    }

    public async Task<int> GetUserCount(GetUserListQuery filterParams, CancellationToken cancellationToken = default)
    {
        int totalCount =
            await QueryByFilter(filterParams)
                .AsNoTracking()
                .CountAsync(cancellationToken);

        return totalCount;
    }

    public async Task<List<UserResponse>> GetUserList(PagingParameters pagingParameters, GetUserListQuery filterParams, CancellationToken cancellationToken = default)
    {
        List<UserResponse> userList =
            await QueryByFilter(filterParams)
                .AsNoTracking()
                .OrderBy(u => u.Username)
                .ApplyPaging(pagingParameters)
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    Username = u.Username,
                })
                .ToListAsync(cancellationToken);

        return userList;
    }

    #region Private Methods
    private IQueryable<User> QueryByEmail(string email)
    {
        IQueryable<User> queryByEmail = 
            context.Users
                   .Where(u => u.Email == email);

        return queryByEmail;
    }

    private IQueryable<User> QueryByFilter(GetUserListQuery filterParams)
    {
        IQueryable<User> queryByFilter =
            context.Users
                   .Where(u => u.Id != filterParams.CallingUserId)
                   .Where(u => u.Username.Contains(filterParams.Username));

        return queryByFilter;
    }
    #endregion
}