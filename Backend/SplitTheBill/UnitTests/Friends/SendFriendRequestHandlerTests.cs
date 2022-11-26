using Application.Friends.SendFriendRequest;

namespace UnitTests.Friends;

public class SendFriendRequestHandlerTests
{
    readonly SendFriendRequestCommandHandler handler;
    readonly SendFriendRequestCommand command = new();

    readonly Mock<IUserRepository> userRepository = new();

    public SendFriendRequestHandlerTests()
    {
        handler = new(userRepository.Object);
    }

    [Fact]
    public async Task AlreadyFriends_ShouldReturnValidationError()
    {
        // Arrange:
        command.SendingUserId = Guid.NewGuid();
        command.ReceivingUserId = Guid.NewGuid();

        UserFriendship userFriendship = new()
        {
            InvitedOn = DateTime.Today,
            AcceptedOn = DateTime.Today,
            RequestSenderId = command.SendingUserId,
            RequestReceiverId = command.ReceivingUserId,
        };

        userRepository.Setup(ur => ur.GetFriendship(command, default))
                      .ReturnsAsync(userFriendship);

        // Act:
        BaseResult<Unit> response = await handler.Handle(command, default);

        // Assert:
        userRepository.Verify(ur => ur.PostFriendRequest(command, default), Times.Never());
        Assert.IsType<ValidationErrorResult<Unit>>(response)
              .ShouldContain(ErrorMessages.Friends.AlreadyFriends);
    }

    [Fact]
    public async Task ValidRequest_ShouldReturnSuccessResult()
    {
        // Arrange:
        command.SendingUserId = Guid.NewGuid();
        command.ReceivingUserId = Guid.NewGuid();

        userRepository.Setup(ur => ur.GetFriendship(command, default))
                      .Returns(Task.FromResult<UserFriendship?>(null));

        // Act:
        BaseResult<Unit> response = await handler.Handle(command, default);

        // Assert:
        userRepository.Verify(ur => ur.PostFriendRequest(command, default), Times.Once());
        Assert.IsType<SuccessResult<Unit>>(response);
    }
}