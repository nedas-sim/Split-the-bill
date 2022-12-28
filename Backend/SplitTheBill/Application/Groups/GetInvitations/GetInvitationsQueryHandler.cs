using Application.Common;
using Application.Repositories;
using Domain.Responses.Groups;

namespace Application.Groups.GetInvitations;

public sealed class GetInvitationsQueryHandler
    : BaseListHandler<GetInvitationsQuery, GroupResponse>
{
    private readonly IGroupRepository groupRepository;

    public GetInvitationsQueryHandler(IGroupRepository groupRepository)
    {
        this.groupRepository = groupRepository;
    }

    public override async Task<List<GroupResponse>> GetResponses(GetInvitationsQuery request, CancellationToken cancellationToken)
        => await groupRepository.GetInvitations(request, cancellationToken);

    public override async Task<int> GetTotalCount(GetInvitationsQuery request, CancellationToken cancellationToken)
        => await groupRepository.GetInvitationCount(request, cancellationToken);
}