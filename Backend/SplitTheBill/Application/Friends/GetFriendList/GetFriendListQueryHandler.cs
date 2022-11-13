using Application.Common;
using Application.Repositories;
using Domain.Common;
using Domain.Common.Results;
using Domain.Responses.Users;
using Domain.Results;
using Microsoft.Extensions.Options;

namespace Application.Friends.GetFriendList;

public sealed class GetFriendListQueryHandler : IListHandler<GetFriendListQuery, UserResponse>
{
    private readonly IUserRepository userRepository;
    private readonly UserSettings config;

    public GetFriendListQueryHandler(IUserRepository userRepository,
                                     IOptions<UserSettings> config)
    {
        this.userRepository = userRepository;
        this.config = config.Value;
    }

    public async Task<BaseListResult<UserResponse>> Handle(GetFriendListQuery request, CancellationToken cancellationToken)
    {
        request.SetConfigurations(config);

        if (request.IsValid(out string? errorMessage) is false)
        {
            return new ListValidationResult<UserResponse>
            {
                Message = errorMessage!,
            };
        }

        List<UserResponse> userResponses =
            await userRepository.GetFriendList(request, request, cancellationToken);

        int userCount = await userRepository.GetFriendCount(request, cancellationToken);

        return new ListResult<UserResponse>(userResponses, userCount, request);
    }
}