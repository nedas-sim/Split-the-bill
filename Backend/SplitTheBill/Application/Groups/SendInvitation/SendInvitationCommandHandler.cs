using Application.Common;
using Application.Repositories;
using Domain.Common;
using Domain.Common.Results;
using Domain.Database;
using Domain.Enums;
using Domain.Extensions;
using MediatR;

namespace Application.Groups.SendInvitation;

public sealed class SendInvitationCommandHandler
    : IResultHandler<SendInvitationCommand, Unit>
{
    private readonly IUserRepository userRepository;
    private readonly IGroupRepository groupRepository;

    public SendInvitationCommandHandler(IUserRepository userRepository, IGroupRepository groupRepository)
    {
        this.userRepository = userRepository;
        this.groupRepository = groupRepository;
    }

    public async Task<BaseResult<Unit>> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
    {
        // Confirm friendship
        bool friendshipExists = await userRepository.ConfirmFriendship(request.SendingUserId, request.ReceivingUserId, cancellationToken);
        if (friendshipExists is false)
        {
            return ErrorMessages.Group.ReceiverIsNotFriend.ToValidationErrorResult<Unit>();
        }

        // Confirm group membership for me
        bool iAmAMemberOfGroup = await groupRepository.IsUserAMember(request.SendingUserId, request.GroupId, cancellationToken);
        if (iAmAMemberOfGroup is false)
        {
            return ErrorMessages.Group.NotAMember.ToValidationErrorResult<Unit>();
        }

        // Confirm group membership for receiver
        UserGroup? membership = await groupRepository.GetUserMembership(request.ReceivingUserId, request.GroupId, cancellationToken);
        GroupMembershipStatus membershipStatus = GroupMembershipStatusHelper.GetStatus(membership);
        string? errorMessage = membershipStatus switch
        {
            GroupMembershipStatus.Member => ErrorMessages.Group.ReceiverIsAlreadyMember,
            GroupMembershipStatus.WaitingForAnswer => ErrorMessages.Group.ReceiverHasAnInvitation,
            _ => null,
        };

        if (errorMessage is not null)
        {
            return errorMessage.ToValidationErrorResult<Unit>();
        }

        // Add invitation
        await groupRepository.SendGroupInvitation(request, cancellationToken);
        return Unit.Value;
    }
}
