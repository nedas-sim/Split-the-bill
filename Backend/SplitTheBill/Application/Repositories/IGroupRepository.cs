using Application.Groups.GetUsersGroupList;
using Domain.Common;
using Domain.Database;
using Domain.Responses.Groups;

namespace Application.Repositories;

public interface IGroupRepository : IBaseRepository<Group>
{
    public Task<List<GroupResponse>> GetUsersGroups(IPaging pagingParameters, GetUsersGroupListQuery query, CancellationToken cancellationToken = default);
    public Task<int> UserGroupsCount(GetUsersGroupListQuery query, CancellationToken cancellationToken = default);
}