using Application.Friends.UpdateFriendRequest;
using Application.Groups.UpdateInvitation;

namespace UnitTests.Groups;

public sealed class UpdateInvitationTests
{
    private readonly UpdateInvitationCommandHandler handler;
    private readonly Mock<IGroupRepository> mock = new();

    public UpdateInvitationTests()
    {
        handler = new(mock.Object);
    }

    [Fact]
    public async Task RequestDoesNotExist_ShouldReturnValidationErrorResult()
    {
        // Arrange:
        UpdateInvitationCommand command = new()
        {
            GroupId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            IsAccepted = true,
        };

        mock.Setup(x => x.GetUserMembership(command.UserId, command.GroupId, It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<UserGroup?>(null));

        // Act:
        BaseResult<Unit> result = await handler.Handle(command, default);

        // Assert:
        Assert.IsType<ValidationErrorResult<Unit>>(result)
              .ShouldContain(ErrorMessages.Group.NotInvited);
    }

    [Fact]
    public async Task RequestAlreadyAccepted_ShouldReturnValidationErrorResult()
    {
        // Arrange:
        UpdateInvitationCommand command = new()
        {
            GroupId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            IsAccepted = true,
        };

        UserGroup userGroup = new()
        {
            AcceptedOn = DateTime.UtcNow,
            InvitedOn = DateTime.UtcNow,
            GroupId = command.GroupId,
            UserId = command.UserId,
        };

        mock.Setup(x => x.GetUserMembership(command.UserId, command.GroupId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userGroup);

        // Act:
        BaseResult<Unit> result = await handler.Handle(command, default);

        // Assert:
        Assert.IsType<ValidationErrorResult<Unit>>(result)
              .ShouldContain(ErrorMessages.Group.AlreadyMember);
    }

    [Fact]
    public async Task RequestIsAccepted_ShouldCallAcceptRequestRepositoryMethod()
    {
        // Arrange:
        UpdateInvitationCommand command = new()
        {
            GroupId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            IsAccepted = true,
        };

        UserGroup userGroup = new()
        {
            AcceptedOn = null,
            InvitedOn = DateTime.UtcNow,
            GroupId = command.GroupId,
            UserId = command.UserId,
        };

        mock.Setup(x => x.GetUserMembership(command.UserId, command.GroupId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userGroup);

        // Act:
        BaseResult<Unit> result = await handler.Handle(command, default);

        // Assert:
        Assert.IsType<SuccessResult<Unit>>(result);
        mock.Verify(m => m.AcceptInvitation(userGroup, default), Times.Once());
        mock.Verify(m => m.RejectInvitation(userGroup, default), Times.Never());
    }

    [Fact]
    public async Task RequestIsRejected_ShouldCallDeleteRequestRepositoryMethod()
    {
        // Arrange:
        UpdateInvitationCommand command = new()
        {
            GroupId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            IsAccepted = false,
        };

        UserGroup userGroup = new()
        {
            AcceptedOn = null,
            InvitedOn = DateTime.UtcNow,
            GroupId = command.GroupId,
            UserId = command.UserId,
        };

        mock.Setup(x => x.GetUserMembership(command.UserId, command.GroupId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userGroup);

        // Act:
        BaseResult<Unit> result = await handler.Handle(command, default);

        // Assert:
        Assert.IsType<SuccessResult<Unit>>(result);
        mock.Verify(m => m.RejectInvitation(userGroup, default), Times.Once());
        mock.Verify(m => m.AcceptInvitation(userGroup, default), Times.Never());
    }
}