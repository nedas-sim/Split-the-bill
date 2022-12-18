using Domain.Views;

namespace Domain.Responses.Groups;

public sealed class GroupResponse
{
    public Guid GroupId { get; set; }
    public string GroupName { get; set; }

    public GroupResponse()
    {

    }
}