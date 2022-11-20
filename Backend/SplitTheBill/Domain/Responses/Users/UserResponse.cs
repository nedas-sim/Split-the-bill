using Domain.Database;
using Domain.Enums;

namespace Domain.Responses.Users;

public sealed class UserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Status => 
        FriendshipStatusHelper.GetStatus(InvitedOn, AcceptedOn, UserSentTheRequest)
                              .ToString();
    public bool CanInvite => Status == FriendshipStatus.NotInvited.ToString();
    public bool CanAccept => Status == FriendshipStatus.ReceivedInvitation.ToString();

    internal DateTime? AcceptedOn { get; set; }
    internal DateTime? InvitedOn { get; set; }
    internal bool UserSentTheRequest { get; set; }

    public UserResponse()
    {

    }

    public UserResponse(User user)
    {
        Id = user.Id;
        Username = user.Username;
    }
}