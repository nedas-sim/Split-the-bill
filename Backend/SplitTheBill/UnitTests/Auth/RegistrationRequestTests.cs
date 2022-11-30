using Application.Authorization.Registration;

namespace UnitTests.Auth;

public class RegistrationRequestTests
{
    readonly RegisterCommand command = new();

    public RegistrationRequestTests()
    {
        command.Config = AuthTestHelper.UserSettings;
    }

    [Fact]
    public void ValidRegistrationForm_ShouldReturnTrue()
    {
        // Arrange:
        command.FillDataWithSamePassword();

        // Act:
        bool isValid = (command as IValidation).IsValid(out _);

        // Assert:
        Assert.True(isValid);
    }

    [Theory]
    [InlineData("email", "password", "password", ErrorMessages.User.InvalidEmail)]
    [InlineData("email", "password", "unmatch", ErrorMessages.User.PasswordMismatch)]
    [InlineData("email", "a", "a", "has to contain")]
    public void InvalidRegistrationFormTests(string email, string password, string repeatPassword, string expectedError)
    {
        // Arrange:
        command.Email = email;
        command.Password = password;
        command.RepeatPassword = repeatPassword;

        // Act:
        bool isValid = (command as IValidation).IsValid(out string? errorMessage);

        // Assert:
        Assert.False(isValid);
        Assert.Contains(expectedError, errorMessage);
    }
}