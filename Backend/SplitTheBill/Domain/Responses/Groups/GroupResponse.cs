using Domain.Views;

namespace Domain.Responses.Groups;

public sealed class GroupResponse
{
    public Guid GroupId { get; set; }
    public string GroupName { get; set; }
    public int MemberCount { get; set; }

    public GroupResponse()
    {

    }

    public GroupResponse(GroupMembershipView view)
    {
        GroupId = view.GroupId;
        GroupName = view.GroupName;
    }
}