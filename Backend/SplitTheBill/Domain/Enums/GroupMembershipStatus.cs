using Domain.Database;

namespace Domain.Enums;

public enum GroupMembershipStatus
{
    Member,
    WaitingForAnswer,
    NotInvited,
}

public static class GroupMembershipStatusHelper
{
    public static GroupMembershipStatus GetStatus(UserGroup? membership)
        => membership switch
        {
            null => GroupMembershipStatus.NotInvited,
            { AcceptedOn: null } => GroupMembershipStatus.WaitingForAnswer,
            _ => GroupMembershipStatus.Member,
        };
}