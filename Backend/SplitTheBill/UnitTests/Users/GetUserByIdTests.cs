using Application.Users.GetUserById;
using Domain.Responses.Users;

namespace UnitTests.Users;

public class GetUserByIdTests
{
    private readonly Mock<IUserRepository> userRepository = new();
    private readonly GetUserByIdQueryHandler handler;

    public GetUserByIdTests()
    {
        handler = new GetUserByIdQueryHandler(userRepository.Object);
    }

    [Fact]
    public async Task ValidId_ShouldReturnUserResponse()
    {
        // Arrange:
        Guid id = Guid.NewGuid();
        UserResponse response = new()
        {
            Id = id,
            Username = "test_username",
        };

        userRepository.Setup(x => x.GetUserResponse(id, default))
                      .ReturnsAsync(response);

        GetUserByIdQuery query = new(id);

        // Act:
        BaseResult<UserResponse> result = await handler.Handle(query, default);

        // Assert:
        SuccessResult<UserResponse> successResult = Assert.IsType<SuccessResult<UserResponse>>(result);
        Assert.Equal(response.Username, successResult.Item.Username);
    }

    [Fact]
    public async Task InvalidId_ShouldReturnNotFoundResult()
    {
        // Arrange:
        Guid id = Guid.NewGuid();
        UserResponse response = new()
        {
            Id = Guid.NewGuid(),
            Username = "test_username",
        };

        userRepository.Setup(x => x.GetUserResponse(id, default))
                      .Returns(Task.FromResult<UserResponse?>(null));

        GetUserByIdQuery query = new(id);

        // Act:
        BaseResult<UserResponse> result = await handler.Handle(query, default);

        // Assert:
        Assert.IsType<NotFoundResult<UserResponse>>(result)
              .ShouldContain(ErrorMessages.User.NotFound);
    }
}