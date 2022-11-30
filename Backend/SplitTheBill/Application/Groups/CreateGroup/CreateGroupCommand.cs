using Application.Common;
using Domain.Common;
using Domain.Database;

namespace Application.Groups.CreateGroup;

public sealed class CreateGroupCommand : BaseCreateRequest<Group>
{
    #region API Params
    public string Name { get; set; }
    #endregion
    #region Auth ID
    internal Guid UserId { get; set; }
    public void SetUserId(Guid id) => UserId = id;
    #endregion
    #region Overrides
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
    #endregion
}