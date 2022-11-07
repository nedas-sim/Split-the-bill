using Domain.Database;

namespace Domain.Enums;

public enum FriendshipStatus
{
    Friends,
    WaitingForAcception,
    ReceivedInvitation,
    NotInvited,
}

public static class FriendshipStatusHelper
{
    public static FriendshipStatus GetStatus(DateTime? invitedOn, DateTime? acceptedOn, bool foundUserIsSender)
        => (invitedOn, acceptedOn) switch
        {
            (null, null) => FriendshipStatus.NotInvited,
            (not null, null) => foundUserIsSender ? FriendshipStatus.ReceivedInvitation : FriendshipStatus.WaitingForAcception,
            (not null, not null) => FriendshipStatus.Friends,
        };

    public static FriendshipStatus GetStatus(UserFriendship friendship, Guid foundUserId)
        => GetStatus(friendship.InvitedOn, friendship.AcceptedOn, friendship.RequestSenderId == foundUserId);
}