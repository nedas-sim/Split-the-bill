using Application.Common;
using Application.Repositories;
using Domain.Common.Results;
using Domain.Database;
using Domain.Results;
using MediatR;

namespace Application.Friends.UpdateFriendRequest;

public sealed class UpdateFriendRequestCommandHandler
    : IResultHandler<UpdateFriendRequestCommand, Unit>
{
    private readonly IUserRepository userRepository;

    public UpdateFriendRequestCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<BaseResult<Unit>> Handle(UpdateFriendRequestCommand request, CancellationToken cancellationToken)
    {
        UserFriendship? friendship = await userRepository.GetFriendship(request, cancellationToken);

        if (DoesFriendRequestExist(friendship) is false)
        {
            return new ValidationErrorResult<Unit>
            {
                Message = "Friend request does not exist",
            };
        }

        Func<UserFriendship, CancellationToken, Task> repositoryCall =
            request.IsAccepted
                ? userRepository.AcceptFriendRequest
                : userRepository.DeleteFriendRequest;

        await repositoryCall(friendship!, cancellationToken);

        return Unit.Value;
    }

    internal static bool DoesFriendRequestExist(UserFriendship? friendship)
    {
        if (friendship is null)
        {
            return false;
        }

        return friendship.AcceptedOn is null;
    }
}