using Application.Repositories;
using Domain.Common.Results;
using Domain.Enums;
using Domain.Results;
using MediatR;

namespace Application.Friends.SendFriendRequest;

public sealed class SendFriendRequestCommandHandler 
    : IRequestHandler<SendFriendRequestCommand, BaseResult<Unit>>
{
    private readonly IUserRepository userRepository;

    public SendFriendRequestCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<BaseResult<Unit>> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
    {
        FriendshipStatus status = await userRepository.GetFriendshipStatus(request, cancellationToken);

        switch (status)
        {
            case FriendshipStatus.Friends:
                return new ValidationErrorResult<Unit>
                {
                    Message = "You are already friends",
                };

            case FriendshipStatus.PendingAcception:
                return new ValidationErrorResult<Unit>
                {
                    Message = "Friend request is already sent",
                };
        }

        await userRepository.PostFriendRequest(request, cancellationToken);
        return Unit.Value;
    }
}