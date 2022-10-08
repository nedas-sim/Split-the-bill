using Domain.Database;
using Domain.Responses.Users;

namespace Application.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    public Task<UserResponse?> GetUserResponse(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> EmailExists(string email);
    public Task<User?> GetByEmail(string email);
}