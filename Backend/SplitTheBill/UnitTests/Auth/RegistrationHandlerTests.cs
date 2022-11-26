using Application.Authorization.Registration;
using Application.Repositories;
using Application.Services;
using Microsoft.Extensions.Options;

namespace UnitTests.Auth;

public class RegistrationHandlerTests
{
	readonly RegisterCommandHandler handler;
	readonly RegisterCommand command = new();

	readonly Mock<IUserRepository> userRepository = new();
	readonly Mock<IAuthorizeService> authorizeService = new();
	readonly Mock<IOptions<UserSettings>> userSettings = new();

	public RegistrationHandlerTests()
	{
		UserSettings config = AuthTestHelper.UserSettings;
        userSettings.Setup(x => x.Value)
					.Returns(config);

		handler = new(userRepository.Object, authorizeService.Object, userSettings.Object);
	}

	[Fact]
	public async Task EmailAlreadyExists_ShouldReturnValidationError()
	{
		// Arrange:
		command.FillDataWithSamePassword();

		userRepository.Setup(ur => ur.EmailExists(command.Email, default))
					  .ReturnsAsync(true);

		// Act:
		BaseResult<CreateResponse> response = await handler.Handle(command, default);

		// Assert:
		userRepository.Verify(ur => ur.EmailExists(command.Email, default), Times.Once());
		ValidationErrorResult<CreateResponse> validationErrorResult = Assert.IsType<ValidationErrorResult<CreateResponse>>(response);
		Assert.Contains(ErrorMessages.User.EmailAlreadyExists, validationErrorResult.Message);
    }

	[Fact]
	public async Task DistinctEmail_ShouldReturnSuccessResult()
	{
		// Arrange:
		command.FillDataWithSamePassword();

        userRepository.Setup(ur => ur.EmailExists(command.Email, default))
                      .ReturnsAsync(false);

        // Act:
        BaseResult<CreateResponse> response = await handler.Handle(command, default);

        // Assert:
        userRepository.Verify(ur => ur.EmailExists(command.Email, default), Times.Once());
		userRepository.Verify(ur => ur.Create(It.IsAny<User>(), default), Times.Once());
		Assert.IsType<SuccessResult<CreateResponse>>(response);
    }
}