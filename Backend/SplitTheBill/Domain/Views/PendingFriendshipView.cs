namespace Domain.Views;

public sealed class PendingFriendshipView
{
    public DateTime InvitedOn { get; set; }

    public Guid RequestSenderId { get; set; }
    public string SenderEmail { get; set; }
    public string SenderUsername { get; set; }

    public Guid RequestReceiverId { get; set; }
    public string ReceiverEmail { get; set; }
    public string ReceiverUsername { get; set; }
}