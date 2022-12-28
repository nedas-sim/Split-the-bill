using Application.Common;
using Application.Repositories;
using Domain.Common;
using Domain.Common.Results;
using Domain.Database;
using Domain.Extensions;
using MediatR;

namespace Application.Groups.UpdateInvitation;

public sealed class UpdateInvitationCommandHandler
    : IResultHandler<UpdateInvitationCommand, Unit>
{
    private readonly IGroupRepository groupRepository;

    public UpdateInvitationCommandHandler(IGroupRepository groupRepository)
    {
        this.groupRepository = groupRepository;
    }

    public async Task<BaseResult<Unit>> Handle(UpdateInvitationCommand request, CancellationToken cancellationToken)
    {
        UserGroup? userGroup = await groupRepository.GetUserMembership(request.UserId, request.GroupId, cancellationToken);

        string? errorMessage = GetErrorMessage(userGroup);

        if (errorMessage is not null)
        {
            return errorMessage.ToValidationErrorResult<Unit>();
        }

        Func<UserGroup, CancellationToken, Task> repositoryCall = 
            request.IsAccepted ?
                groupRepository.AcceptInvitation :
                groupRepository.RejectInvitation;

        await repositoryCall(userGroup!, cancellationToken);
        return Unit.Value;
    }

    internal static string? GetErrorMessage(UserGroup? userGroup)
        => userGroup switch
        {
            null => ErrorMessages.Group.NotInvited,
            { AcceptedOn: not null } => ErrorMessages.Group.AlreadyMember,
            _ => null,
        };
}