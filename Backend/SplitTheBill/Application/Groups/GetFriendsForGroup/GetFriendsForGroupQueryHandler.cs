using Application.Common;
using Application.Repositories;
using Domain.Common;
using Domain.Exceptions;
using Domain.Responses.Users;

namespace Application.Groups.GetFriendsForGroup;

public sealed class GetFriendsForGroupQueryHandler
    : BaseListHandler<GetFriendsForGroupQuery, UserResponse>
{
    private readonly IUserRepository userRepository;
    private readonly IGroupRepository groupRepository;

    public GetFriendsForGroupQueryHandler(IUserRepository userRepository, IGroupRepository groupRepository)
    {
        this.userRepository = userRepository;
        this.groupRepository = groupRepository;
    }

    public override async Task DatabaseValidation(GetFriendsForGroupQuery request, CancellationToken cancellationToken)
    {
        bool userIsAMember = await groupRepository.IsUserAMember(request.UserId, request.GroupId, cancellationToken);

        if (userIsAMember is false)
        {
            throw new ValidationErrorException(ErrorMessages.Group.NotAMember);
        }
    }

    public override async Task<List<UserResponse>> GetResponses(GetFriendsForGroupQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetFriendSuggestionsForGroup(request, cancellationToken);
    }

    public override async Task<int> GetTotalCount(GetFriendsForGroupQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetFriendSuggestionsForGroupCount(request, cancellationToken);
    }
}