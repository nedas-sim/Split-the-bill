using Domain.Database;

namespace Application.Repositories;

public interface IGroupRepository : IBaseRepository<Group>
{
    public Task<bool> GroupNameExists(string name, CancellationToken cancellationToken = default);
}