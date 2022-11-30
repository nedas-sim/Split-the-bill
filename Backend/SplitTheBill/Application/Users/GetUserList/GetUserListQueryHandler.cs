using Application.Common;
using Application.Repositories;
using Domain.Common;
using Domain.Responses.Users;
using Microsoft.Extensions.Options;

namespace Application.Users.GetUserList;

public sealed class GetUserListQueryHandler : BaseListHandler<GetUserListQuery, UserResponse>
{
    private readonly IUserRepository userRepository;
    private readonly UserSettings config;

    public GetUserListQueryHandler(IUserRepository userRepository,
                                   IOptions<UserSettings> config)
    {
        this.userRepository = userRepository;
        this.config = config.Value;
    }

    public override async Task PreValidation(GetUserListQuery request)
    {
        request.Config = config;
    }

    public override async Task<List<UserResponse>> GetResponses(GetUserListQuery request, CancellationToken cancellationToken)
        => await userRepository.GetUserList(request, request, cancellationToken);

    public override async Task<int> GetTotalCount(GetUserListQuery request, CancellationToken cancellationToken)
        => await userRepository.GetUserCount(request, cancellationToken);
}