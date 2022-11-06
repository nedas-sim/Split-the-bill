using Application.Groups.CreateGroup;
using Application.Repositories;
using Domain.Common.Results;
using Domain.Responses;
using Domain.Results;
using Moq;

namespace UnitTests.Groups;

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
        BaseResult<CreateResponse> result = await handler.Handle(command, default);
        bool isValid = command.IsValid(out _);

        // Assert:
        Assert.False(isValid);
        Assert.IsType<ValidationErrorResult<CreateResponse>>(result);

    }

    [Fact]
    public async Task ValidData_ShouldReturnSuccessResult()
    {
        // Arrange:
        CreateGroupCommand command = new()
        {
            Name = "valid name",
            UserId = Guid.NewGuid(),
        };

        // Act:
        BaseResult<CreateResponse> result = await handler.Handle(command, default);
        bool isValid = command.IsValid(out _);

        // Assert:
        Assert.True(isValid);
        Assert.IsType<SuccessResult<CreateResponse>>(result);
    }
}