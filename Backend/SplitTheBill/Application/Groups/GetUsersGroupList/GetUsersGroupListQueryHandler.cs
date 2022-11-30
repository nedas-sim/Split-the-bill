using Application.Common;
using Application.Repositories;
using Domain.Responses.Groups;

namespace Application.Groups.GetUsersGroupList;

public sealed class GetUsersGroupListQueryHandler : BaseListHandler<GetUsersGroupListQuery, GroupResponse>
{
    private readonly IGroupRepository groupRepository;

    public GetUsersGroupListQueryHandler(IGroupRepository groupRepository)
    {
        this.groupRepository = groupRepository;
    }

    public override async Task<List<GroupResponse>> GetResponses(GetUsersGroupListQuery request, CancellationToken cancellationToken)
        => await groupRepository.GetUsersGroups(request, request.UserId, cancellationToken);

    public override async Task<int> GetTotalCount(GetUsersGroupListQuery request, CancellationToken cancellationToken)
        => await groupRepository.UserGroupsCount(request.UserId, cancellationToken);
}