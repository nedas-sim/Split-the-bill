using Application.Authorization.Registration;
using Domain.Common;

namespace UnitTests.Auth;

public class RegistrationTests
{
    readonly RegisterCommand command = new();

    public RegistrationTests()
    {
        command.SetConfigurations(new UserSettings { MinPasswordLength = 6 });
    }

    [Fact]
    public void ValidRegistrationForm_ShouldReturnTrue()
    {
        // Arrange:
        command.Email = "email@email.com";
        command.Password = "password";
        command.RepeatPassword = "password";

        // Act:
        bool isValid = command.IsValid(out _);

        // Assert:
        Assert.True(isValid);
    }

    [Theory]
    [InlineData("email", "password", "password", RegisterCommand.InvalidEmailErrorMessage)]
    [InlineData("email", "password", "unmatch", RegisterCommand.PasswordMismatchErrorMessage)]
    [InlineData("email", "a", "a", "has to contain")]
    public void InvalidRegistrationFormTests(string email, string password, string repeatPassword, string expectedError)
    {
        // Arrange:
        command.Email = email;
        command.Password = password;
        command.RepeatPassword = repeatPassword;

        // Act:
        bool isValid = command.IsValid(out string? errorMessage);

        // Assert:
        Assert.False(isValid);
        Assert.Contains(expectedError, errorMessage);
    }
}