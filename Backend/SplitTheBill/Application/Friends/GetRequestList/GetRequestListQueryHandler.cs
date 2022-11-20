using Application.Common;
using Application.Repositories;
using Domain.Common.Results;
using Domain.Responses.Users;
using Domain.Results;

namespace Application.Friends.GetRequestList;

public sealed class GetRequestListQueryHandler : IListHandler<GetRequestListQuery, UserResponse>
{
    private readonly IUserRepository userRepository;

    public GetRequestListQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<BaseListResult<UserResponse>> Handle(GetRequestListQuery request, CancellationToken cancellationToken)
    {
        List<UserResponse> userResponses =
            await userRepository.GetPendingFriendshipList(request, request, cancellationToken);

        int friendRequestCount = await userRepository.GetPendingFriendshipCount(request, cancellationToken);

        return new ListResult<UserResponse>(userResponses, friendRequestCount, request);
    }
}