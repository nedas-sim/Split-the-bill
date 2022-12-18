using Application.Common;
using Application.Repositories;
using Domain.Common;
using Domain.Exceptions;
using Domain.Responses.Groups;

namespace Application.Groups.GetGroupsForFriend;

public sealed class GetGroupsForFriendQueryHandler : BaseListHandler<GetGroupsForFriendQuery, GroupResponse>
{
    private readonly IUserRepository userRepository;
    private readonly IGroupRepository groupRepository;

    public GetGroupsForFriendQueryHandler(IUserRepository userRepository, IGroupRepository groupRepository)
    {
        this.userRepository = userRepository;
        this.groupRepository = groupRepository;
    }

    public override async Task DatabaseValidation(GetGroupsForFriendQuery request, CancellationToken cancellationToken)
    {
        bool isFriend = await userRepository.ConfirmFriendship(request.UserId, request.FriendId, cancellationToken);

        if (isFriend is false)
        {
            throw new ValidationErrorException(ErrorMessages.Friends.NotAFriend);
        }
    }

    public override async Task<List<GroupResponse>> GetResponses(GetGroupsForFriendQuery request, CancellationToken cancellationToken)
        => await groupRepository.GetPotentialGroupsForFriend(request, cancellationToken);

    public override async Task<int> GetTotalCount(GetGroupsForFriendQuery request, CancellationToken cancellationToken)
        => await groupRepository.GetPotentialGroupsForFriendCount(request, cancellationToken);
}