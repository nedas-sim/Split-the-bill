using Application.Groups.CreateGroup;
using Application.Repositories;
using Domain.Common.Results;
using Domain.Results;
using MediatR;
using Moq;

namespace CrudTests.Groups;

public class CreateGroupTests
{
    private readonly CreateGroupCommandHandler handler;
    private readonly Mock<IGroupRepository> mock;

    public CreateGroupTests()
    {
        mock = new Mock<IGroupRepository>();
        handler = new CreateGroupCommandHandler(mock.Object);
    }

    [Fact]
    public async Task InvalidRequest_ShouldReturnValidationErrorResult()
    {
        // Arrange:
        CreateGroupCommand command = new()
        {
            Name = "",
            UserId = Guid.NewGuid(),
        };

        // Act:
        BaseResult<Unit> result = await handler.Handle(command, default);

        // Assert:
        Assert.IsType<ValidationErrorResult<Unit>>(result);
    }

    [Fact]
    public async Task GroupNameAlreadyExists_ShouldReturnValidationErrorResult()
    {
        // Arrange:
        CreateGroupCommand command = new()
        {
            Name = "existing name",
            UserId = Guid.NewGuid(),
        };

        mock.Setup(x => x.GroupNameExists(command.Name, default))
            .ReturnsAsync(true);

        // Act:
        BaseResult<Unit> result = await handler.Handle(command, default);

        // Assert:
        Assert.IsType<ValidationErrorResult<Unit>>(result);
    }

    [Fact]
    public async Task ValidData_ShouldReturnNoContentResult()
    {
        // Arrange:
        CreateGroupCommand command = new()
        {
            Name = "valid name",
            UserId = Guid.NewGuid(),
        };

        mock.Setup(x => x.GroupNameExists(command.Name, default))
            .ReturnsAsync(false);

        // Act:
        BaseResult<Unit> result = await handler.Handle(command, default);

        // Assert:
        Assert.IsType<NoContentResult<Unit>>(result);
    }
}