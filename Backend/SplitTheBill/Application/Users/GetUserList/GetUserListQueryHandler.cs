using Application.Common;
using Application.Repositories;
using Domain.Common;
using Domain.Common.Results;
using Domain.Responses.Users;
using Domain.Results;
using Microsoft.Extensions.Options;

namespace Application.Users.GetUserList;

public sealed class GetUserListQueryHandler : IListHandler<GetUserListQuery, UserResponse>
{
    private readonly IUserRepository userRepository;
    private readonly UserSettings config;

    public GetUserListQueryHandler(IUserRepository userRepository,
                                   IOptions<UserSettings> config)
    {
        this.userRepository = userRepository;
        this.config = config.Value;
    }

    public async Task<BaseListResult<UserResponse>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        request.Config = config;

        if (request.IsValid(out string? errorMessage) is false)
        {
            return new ListValidationResult<UserResponse>
            {
                Message = errorMessage!,
            };
        }

        List<UserResponse> userResponses =
            await userRepository.GetUserList(request, request, cancellationToken);

        int userCount = await userRepository.GetUserCount(request, cancellationToken);

        return new ListResult<UserResponse>(userResponses, userCount, request);
    }
}