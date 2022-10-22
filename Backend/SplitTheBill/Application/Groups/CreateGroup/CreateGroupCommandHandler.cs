using Application.Common;
using Application.Repositories;
using Domain.Database;
using Domain.Exceptions;

namespace Application.Groups.CreateGroup;

public sealed class CreateGroupCommandHandler : BaseCreateHandler<CreateGroupCommand, Group>
{
    private readonly IGroupRepository groupRepository;

    public CreateGroupCommandHandler(IGroupRepository groupRepository) : base(groupRepository)
    {
        this.groupRepository = groupRepository;
    }

    public override async Task DatabaseValidation(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        bool groupNameExists = await groupRepository.GroupNameExists(request.Name, cancellationToken);
        if (groupNameExists)
        {
            throw new ValidationErrorException($"Group with name {request.Name} already exists");
        }
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