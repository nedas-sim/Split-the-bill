using Application.Common;
using Application.Repositories;
using Domain.Responses.Users;

namespace Application.Friends.GetRequestList;

public sealed class GetRequestListQueryHandler : BaseListHandler<GetRequestListQuery, UserResponse>
{
    private readonly IUserRepository userRepository;

    public GetRequestListQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public override async Task<List<UserResponse>> GetResponses(GetRequestListQuery request, CancellationToken cancellationToken)
        => await userRepository.GetPendingFriendshipList(request, request, cancellationToken);

    public override async Task<int> GetTotalCount(GetRequestListQuery request, CancellationToken cancellationToken)
        => await userRepository.GetPendingFriendshipCount(request, cancellationToken);
}