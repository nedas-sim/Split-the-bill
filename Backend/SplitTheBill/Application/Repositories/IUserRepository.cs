using Domain.Database;
using Domain.Responses.Users;

namespace Application.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    public Task<UserResponse?> GetUserResponse(Guid id, CancellationToken cancellationToken = default);
}