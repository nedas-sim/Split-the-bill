using Application.Common;
using Application.Repositories;
using Domain.Common.Results;
using Domain.Responses.Users;
using Domain.Results;

namespace Application.Friends.GetFriendList;

public sealed class GetFriendListQueryHandler : IListHandler<GetFriendListQuery, UserResponse>
{
    private readonly IUserRepository userRepository;

    public GetFriendListQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<BaseListResult<UserResponse>> Handle(GetFriendListQuery request, CancellationToken cancellationToken)
    {
        List<UserResponse> userResponses =
            await userRepository.GetFriendList(request, request, cancellationToken);

        int userCount = await userRepository.GetFriendCount(request, cancellationToken);

        return new ListResult<UserResponse>(userResponses, userCount, request);
    }
}