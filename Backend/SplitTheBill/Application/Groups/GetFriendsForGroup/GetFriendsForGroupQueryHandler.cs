using Application.Common;
using Application.Repositories;
using Domain.Responses.Users;

namespace Application.Groups.GetFriendsForGroup;

public sealed class GetFriendsForGroupQueryHandler
    : BaseListHandler<GetFriendsForGroupQuery, UserResponse>
{
    private readonly IUserRepository userRepository;

    public GetFriendsForGroupQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
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