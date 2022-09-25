using Application.Repositories;
using Domain.Database;
using Domain.Responses.Users;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class UserRepository : BaseRepository<User, UserId>, IUserRepository
{
    private readonly DataContext context;

    public UserRepository(DataContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<UserResponse?> GetUserResponse(UserId id)
    {
        UserResponse? user =
            await context.Users
                         .Select(u => new UserResponse
                         {
                             Id = u.Id,
                             Username = u.Username,
                         })
                         .FirstOrDefaultAsync(u => u.Id == id);

        return user;
    }
}