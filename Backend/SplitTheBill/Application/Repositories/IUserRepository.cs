using Domain.Database;
using Domain.Responses.Users;

namespace Application.Repositories;

public interface IUserRepository : IBaseRepository<User, UserId>
{
    public Task<UserResponse?> GetUserResponse(UserId id);
}