using Application.Repositories;
using Domain.Common.Results;
using Domain.Database;
using Domain.Results;
using MediatR;

namespace Application.Groups.CreateGroup;

public sealed class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, BaseResult<Unit>>
{
    private readonly IGroupRepository groupRepository;

    public CreateGroupCommandHandler(IGroupRepository groupRepository)
    {
        this.groupRepository = groupRepository;
    }

    public async Task<BaseResult<Unit>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        if (request.IsValid(out string? errorMessage) is false)
        {
            return new ValidationErrorResult<Unit>
            {
                Message = errorMessage!,
            };
        }

        bool groupNameExists = await groupRepository.GroupNameExists(request.Name, cancellationToken);
        if (groupNameExists)
        {
            return new ValidationErrorResult<Unit>
            {
                Message = $"Group with name {request.Name} already exists",
            };
        }

        Group group = request.BuildEntity();
        group.UserGroups.Add(new UserGroup
        {
            UserId = request.UserId,
            InvitedOn = group.CreatedOn,
            AcceptedOn = group.CreatedOn,
        });

        await groupRepository.Create(group, cancellationToken);
        return new NoContentResult<Unit>();
    }
}