using Application.Common;
using Application.Repositories;
using Domain.Common;
using Domain.Common.Results;
using Domain.Responses.Users;
using Domain.Results;
using Microsoft.Extensions.Options;

namespace Application.Friends.GetRequestList;

public sealed class GetRequestListQueryHandler : IListHandler<GetRequestListQuery, UserResponse>
{
    private readonly IUserRepository userRepository;
    private readonly UserSettings config;

    public GetRequestListQueryHandler(IUserRepository userRepository,
                                      IOptions<UserSettings> config)
    {
        this.userRepository = userRepository;
        this.config = config.Value;
    }

    public async Task<BaseListResult<UserResponse>> Handle(GetRequestListQuery request, CancellationToken cancellationToken)
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
            await userRepository.GetPendingFriendshipList(request, request, cancellationToken);

        int friendRequestCount = await userRepository.GetPendingFriendshipCount(request, cancellationToken);

        return new ListResult<UserResponse>(userResponses, friendRequestCount, request);
    }
}