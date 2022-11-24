using Application.Common;
using Domain.Common;
using Domain.Database;

namespace Application.Groups.CreateGroup;

public sealed class CreateGroupCommand : BaseCreateRequest<Group>
{
    public string Name { get; set; }

    internal Guid UserId { get; set; }
    public void SetUserId(Guid id) => UserId = id;

    public override string ApiErrorMessagePrefix => ErrorMessages.Group.CreateRequestPrefix;

    public override Group BuildEntity()
    {
        Group group = new()
        {
            Name = Name,
            UserGroups = new List<UserGroup>(),
        };

        return group;
    }

    public override IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        bool validName = string.IsNullOrWhiteSpace(Name) is false;

        yield return (validName, ErrorMessages.Group.EmptyName);
    }
}