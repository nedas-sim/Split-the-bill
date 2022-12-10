using Application.Groups.SendInvitation;

namespace UnitTests.Groups;

public sealed class SendInvitationTests
{
    readonly SendInvitationCommandHandler handler;
    readonly SendInvitationCommand command = new();

    readonly Mock<IUserRepository> userRepository = new();
    readonly Mock<IGroupRepository> groupRepository = new();

    public SendInvitationTests()
    {
        handler = new(userRepository.Object, groupRepository.Object);
    }

    private void SetupIds()
    {
        command.ReceivingUserId = Guid.NewGuid();
        command.SendingUserId = Guid.NewGuid();
        command.GroupId = Guid.NewGuid();
    }

    private void SetupConfirmFriendship(bool returnValue)
    {
        userRepository.Setup(ur => ur.ConfirmFriendship(command.SendingUserId, command.ReceivingUserId, default))
                      .ReturnsAsync(returnValue);
    }

    private void SetupIsUserAMember(bool returnValue)
    {
        groupRepository.Setup(gr => gr.IsUserAMember(command.SendingUserId, command.GroupId, default))
                       .ReturnsAsync(returnValue);
    }

    [Fact]
    public async Task ReceiverIsNotMyFriend_ShouldReturnValidationError()
    {
        // Arrange:
        SetupIds();
        SetupConfirmFriendship(false);
        
        // Act:
        BaseResult<Unit> result = await handler.Handle(command, default);

        // Assert:
        userRepository.Verify(ur => ur.ConfirmFriendship(command.SendingUserId, command.ReceivingUserId, default), Times.Once());
        Assert.IsType<ValidationErrorResult<Unit>>(result)
              .ShouldContain(ErrorMessages.Group.ReceiverIsNotFriend);
    }

    [Fact]
    public async Task SenderIsNotAMember_ShouldReturnValidationError()
    {
        // Arrange:
        SetupIds();
        SetupConfirmFriendship(true);
        SetupIsUserAMember(false);

        // Act:
        BaseResult<Unit> result = await handler.Handle(command, default);

        // Assert:
        groupRepository.Verify(gr => gr.IsUserAMember(command.SendingUserId, command.GroupId, default), Times.Once());
        Assert.IsType<ValidationErrorResult<Unit>>(result)
              .ShouldContain(ErrorMessages.Group.NotAMember);
    }

    [Fact]
    public async Task ReceiverIsAlreadyAMember_ShouldReturnValidationError()
    {
        // Arrange:
        SetupIds();
        SetupConfirmFriendship(true);
        SetupIsUserAMember(true);
        
        UserGroup? userMembership = new()
        {
            AcceptedOn = DateTime.Now,
        };
        groupRepository.Setup(gr => gr.GetUserMembership(command.ReceivingUserId, command.GroupId, default))
                       .ReturnsAsync(userMembership);

        // Act:
        BaseResult<Unit> result = await handler.Handle(command, default);

        // Assert:
        groupRepository.Verify(gr => gr.GetUserMembership(command.ReceivingUserId, command.GroupId, default), Times.Once());
        Assert.IsType<ValidationErrorResult<Unit>>(result)
              .ShouldContain(ErrorMessages.Group.ReceiverIsAlreadyMember);
    }

    [Fact]
    public async Task NoValidationErrors_ShouldReturnSuccessResult()
    {
        // Arrange:
        SetupIds();
        SetupConfirmFriendship(true);
        SetupIsUserAMember(true);

        UserGroup? userMembership = null;
        groupRepository.Setup(gr => gr.GetUserMembership(command.ReceivingUserId, command.GroupId, default))
                       .ReturnsAsync(userMembership);

        // Act:
        BaseResult<Unit> result = await handler.Handle(command, default);

        // Assert:
        groupRepository.Verify(gr => gr.SendGroupInvitation(command, default), Times.Once());
        Assert.IsType<SuccessResult<Unit>>(result);
    }
}