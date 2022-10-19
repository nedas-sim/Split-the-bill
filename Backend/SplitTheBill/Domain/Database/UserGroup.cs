namespace Domain.Database;

public sealed class UserGroup
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid GroupId { get; set; }
    public Group Group { get; set; }

    public bool IsAdmin { get; set; }
    public DateTime InvitedOn { get; set; }
    public DateTime? AcceptedOn { get; set; }
    public DateTime? RejectedOn { get; set; }
}