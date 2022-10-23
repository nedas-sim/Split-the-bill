using Application.Common;
using Application.Repositories;
using Domain.Responses.Groups;
using Domain.Results;

namespace Application.Groups.GetUsersGroupList;

public sealed class GetUsersGroupListQueryHandler : IListHandler<GetUsersGroupListQuery, GroupResponse>
{
    private readonly IGroupRepository groupRepository;

    public GetUsersGroupListQueryHandler(IGroupRepository groupRepository)
    {
        this.groupRepository = groupRepository;
    }

    public async Task<ListResult<GroupResponse>> Handle(GetUsersGroupListQuery request, CancellationToken cancellationToken)
    {
        List<GroupResponse> userGroups = 
            await groupRepository.GetUsersGroups(request, request.UserId, cancellationToken);

        int groupCount = await groupRepository.UserGroupsCount(request.UserId, cancellationToken);

        return new ListResult<GroupResponse>(userGroups, groupCount, request);
    }
}