using Application.Common;
using Domain.Common;
using Domain.Database;
using Domain.Extensions;

namespace Application.Groups.CreateGroup;

public sealed class CreateGroupCommand : BaseCreateRequest<Group>
{
    public string Name { get; set; }

    public Guid UserId { get; internal set; }
    public void SetUserId(Guid id) => UserId = id;

    public override Group BuildEntity()
    {
        Group group = new()
        {
            Name = Name,
            UserGroups = new List<UserGroup>(),
        };

        return group;
    }

    public override bool IsValid(out string? errorMessage)
    {
        bool emptyName = string.IsNullOrWhiteSpace(Name);

        List<string> errorMessages = new();
        errorMessages.AddIfFalse(emptyName is false, ErrorMessages.Group.EmptyName);

        errorMessage = errorMessages.BuildErrorMessage(ErrorMessages.Group.CreateRequestPrefix);
        return string.IsNullOrEmpty(errorMessage);
    }
}