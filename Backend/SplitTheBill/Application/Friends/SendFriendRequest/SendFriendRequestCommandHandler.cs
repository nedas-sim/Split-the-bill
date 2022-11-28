using Application.Repositories;
using Domain.Common;
using Domain.Common.Results;
using Domain.Database;
using Domain.Enums;
using Domain.Extensions;
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
            string? errorMessage = GetErrorMessageForStatus(status);
            if (errorMessage is not null)
            {
                return errorMessage.ToValidationErrorResult<Unit>();
            }
        }

        await userRepository.PostFriendRequest(request, cancellationToken);
        return Unit.Value;
    }

    private static string? GetErrorMessageForStatus(FriendshipStatus status)
        => status switch
        {
            FriendshipStatus.Friends => ErrorMessages.Friends.AlreadyFriends,
            FriendshipStatus.WaitingForAcception => ErrorMessages.Friends.RequestSent,
            FriendshipStatus.ReceivedInvitation => ErrorMessages.Friends.RequestReceived,
            _ => null,
        };
}