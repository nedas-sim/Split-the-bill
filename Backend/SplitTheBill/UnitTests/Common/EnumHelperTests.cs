using Domain.Enums;

namespace UnitTests.Common;

public sealed class EnumHelperTests
{
    [Theory]
    [InlineData(false, false, GroupMembershipStatus.NotInvited)]
    [InlineData(true, false, GroupMembershipStatus.WaitingForAnswer)]
    [InlineData(true, true, GroupMembershipStatus.Member)]
    public void GroupMembershipStatusCases(bool userGroupExists, bool acceptedOnExists, GroupMembershipStatus expected)
    {
        // Arrange:
        UserGroup? membership = userGroupExists 
            ? new UserGroup
            {
                AcceptedOn = acceptedOnExists ? DateTime.Now : null,
            }
            : null;

        // Act:
        GroupMembershipStatus actual = GroupMembershipStatusHelper.GetStatus(membership);

        // Assert:
        Assert.Equal(expected, actual);
    }
}