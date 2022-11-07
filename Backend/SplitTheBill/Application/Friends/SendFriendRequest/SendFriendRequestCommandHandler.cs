using Application.Repositories;
using Domain.Common.Results;
using Domain.Database;
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
        UserFriendship? friendship = await userRepository.GetFriendship(request, cancellationToken);

        if (friendship is not null)
        {
            FriendshipStatus status = FriendshipStatusHelper.GetStatus(friendship, request.ReceivingUserId);
            switch (status)
            {
                case FriendshipStatus.Friends:
                    return new ValidationErrorResult<Unit>
                    {
                        Message = "You are already friends",
                    };

                case FriendshipStatus.WaitingForAcception:
                    return new ValidationErrorResult<Unit>
                    {
                        Message = "Friend request is already sent",
                    };

                case FriendshipStatus.ReceivedInvitation:
                    return new ValidationErrorResult<Unit>
                    {
                        Message = "You have already received an invitation from this user",
                    };
            }
        }

        await userRepository.PostFriendRequest(request, cancellationToken);
        return Unit.Value;
    }
}