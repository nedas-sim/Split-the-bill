using Domain.Common;
using Domain.Database;
using Domain.Responses.Groups;

namespace Application.Repositories;

public interface IGroupRepository : IBaseRepository<Group>
{
    public Task<List<GroupResponse>> GetUsersGroups(IPaging pagingParameters, Guid userId, CancellationToken cancellationToken = default);
    public Task<int> UserGroupsCount(Guid userId, CancellationToken cancellationToken = default);
}