namespace Domain.Views;

public sealed class GroupMembershipView
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    public string Email { get; set; }
    public string GroupName { get; set; }
    public DateTime AcceptedOn { get; set; }
}