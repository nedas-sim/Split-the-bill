namespace Domain.Database;

public sealed class UserFriendship
{
    public Guid RequestSenderId { get; set; }
    public User RequestSender { get; set; }

    public Guid RequestReceiverId { get; set; }
    public User RequestReceiver { get; set; }

    public DateTime InvitedOn { get; set; }
    public DateTime? AcceptedOn { get; set; }
}