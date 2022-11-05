using Application.Common;
using Application.Repositories;
using Domain.Database;

namespace Application.Groups.CreateGroup;

public sealed class CreateGroupCommandHandler : BaseCreateHandler<CreateGroupCommand, Group>
{
    public CreateGroupCommandHandler(IGroupRepository groupRepository) : base(groupRepository)
    {
    }

    public override async Task PostEntityBuilding(CreateGroupCommand request, Group entity, CancellationToken cancellationToken)
    {
        entity.UserGroups.Add(new UserGroup
        {
            UserId = request.UserId,
            InvitedOn = entity.CreatedOn,
            AcceptedOn = entity.CreatedOn,
        });
    }
}