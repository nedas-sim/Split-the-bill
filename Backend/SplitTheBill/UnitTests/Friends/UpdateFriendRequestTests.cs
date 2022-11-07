using Application.Friends.UpdateFriendRequest;
using Application.Repositories;
using Domain.Common.Results;
using Domain.Database;
using Domain.Results;
using MediatR;
using Moq;

namespace UnitTests.Friends;

public class UpdateFriendRequestTests
{
    private readonly UpdateFriendRequestCommandHandler handler;
    private readonly Mock<IUserRepository> mock;

    public UpdateFriendRequestTests()
    {
        mock = new Mock<IUserRepository>();
        handler = new(mock.Object);
    }

    [Fact]
    public async Task RequestDoesNotExist_ShouldReturnValidationErrorResult()
    {
        // Arrange:
        UpdateFriendRequestCommand command = new()
        {
            SenderId = Guid.NewGuid(),
            ReceiverId = Guid.NewGuid(),
            IsAccepted = true,
        };

        mock.Setup(x => x.GetFriendship(It.IsAny<UpdateFriendRequestCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<UserFriendship?>(null));

        // Act:
        BaseResult<Unit> result = await handler.Handle(command, default);

        // Assert:
        Assert.IsType<ValidationErrorResult<Unit>>(result);
    }

    [Fact]
    public async Task RequestAlreadyAccepted_ShouldReturnValidationErrorResult()
    {
        // Arrange:
        UpdateFriendRequestCommand command = new()
        {
            SenderId = Guid.NewGuid(),
            ReceiverId = Guid.NewGuid(),
            IsAccepted = true,
        };

        UserFriendship userFriendship = new()
        {
            AcceptedOn = DateTime.UtcNow,
            InvitedOn = DateTime.UtcNow,
            RequestReceiverId = command.ReceiverId,
            RequestSenderId = command.SenderId,
        };

        mock.Setup(x => x.GetFriendship(
            It.IsAny<UpdateFriendRequestCommand>(), 
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(userFriendship);

        // Act:
        BaseResult<Unit> result = await handler.Handle(command, default);

        // Assert:
        Assert.IsType<ValidationErrorResult<Unit>>(result);
    }

    [Fact]
    public async Task RequestIsAccepted_ShouldCallAcceptRequestRepositoryMethod()
    {
        // Arrange:
        UpdateFriendRequestCommand command = new()
        {
            SenderId = Guid.NewGuid(),
            ReceiverId = Guid.NewGuid(),
            IsAccepted = true,
        };

        UserFriendship userFriendship = new()
        {
            AcceptedOn = null,
            InvitedOn = DateTime.UtcNow,
            RequestReceiverId = command.ReceiverId,
            RequestSenderId = command.SenderId,
        };

        mock.Setup(x => x.GetFriendship(
            It.IsAny<UpdateFriendRequestCommand>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(userFriendship);

        // Act:
        BaseResult<Unit> result = await handler.Handle(command, default);

        // Assert:
        Assert.IsType<SuccessResult<Unit>>(result);
        mock.Verify(m => m.AcceptFriendRequest(userFriendship, default), Times.Once());
    }

    [Fact]
    public async Task RequestIsRejected_ShouldCallDeleteRequestRepositoryMethod()
    {
        // Arrange:
        UpdateFriendRequestCommand command = new()
        {
            SenderId = Guid.NewGuid(),
            ReceiverId = Guid.NewGuid(),
            IsAccepted = false,
        };

        UserFriendship userFriendship = new()
        {
            AcceptedOn = null,
            InvitedOn = DateTime.UtcNow,
            RequestReceiverId = command.ReceiverId,
            RequestSenderId = command.SenderId,
        };

        mock.Setup(x => x.GetFriendship(
            It.IsAny<UpdateFriendRequestCommand>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(userFriendship);

        // Act:
        BaseResult<Unit> result = await handler.Handle(command, default);

        // Assert:
        Assert.IsType<SuccessResult<Unit>>(result);
        mock.Verify(m => m.DeleteFriendRequest(userFriendship, default), Times.Once());
    }
}