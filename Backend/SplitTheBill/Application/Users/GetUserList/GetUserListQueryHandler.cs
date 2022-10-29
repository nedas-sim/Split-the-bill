using Application.Common;
using Application.Repositories;
using Domain.Responses.Users;
using Domain.Results;

namespace Application.Users.GetUserList;

public sealed class GetUserListQueryHandler : IListHandler<GetUserListQuery, UserResponse>
{
    private readonly IUserRepository userRepository;

    public GetUserListQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<ListResult<UserResponse>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        /*if (request.Username.Length < 3)
        {
            return new ValidationErrorResult<UserResponse>
            {
                Message = "",
            };
        }*/

        List<UserResponse> userResponses =
            await userRepository.GetUserList(request, request, cancellationToken);

        int userCount = await userRepository.GetUserCount(request, cancellationToken);

        return new ListResult<UserResponse>(userResponses, userCount, request);
    }
}