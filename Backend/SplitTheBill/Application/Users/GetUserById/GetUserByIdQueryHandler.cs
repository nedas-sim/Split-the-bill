using Application.Common;
using Application.Repositories;
using Domain.Common;
using Domain.Common.Results;
using Domain.Extensions;
using Domain.Responses.Users;

namespace Application.Users.GetUserById;

public sealed class GetUserByIdQueryHandler : IResultHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResult<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        UserResponse? user = await _userRepository.GetUserResponse(request.Id, cancellationToken);

        if (user is null)
        {
            return ErrorMessages.User.NotFound.ToNotFoundResult<UserResponse>();
        }

        return user;
    }
}