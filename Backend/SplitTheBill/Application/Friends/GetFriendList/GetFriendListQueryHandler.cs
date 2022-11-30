using Application.Common;
using Application.Repositories;
using Domain.Responses.Users;

namespace Application.Friends.GetFriendList;

public sealed class GetFriendListQueryHandler : BaseListHandler<GetFriendListQuery, UserResponse>
{
    private readonly IUserRepository userRepository;

    public GetFriendListQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public override async Task<List<UserResponse>> GetResponses(GetFriendListQuery request, CancellationToken cancellationToken)
        => await userRepository.GetFriendList(request, request, cancellationToken);

    public override async Task<int> GetTotalCount(GetFriendListQuery request, CancellationToken cancellationToken)
        => await userRepository.GetFriendCount(request, cancellationToken);
}