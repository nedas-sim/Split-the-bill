using Application.Common;
using Domain.Common;
using Domain.Database;
using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Registration;

public sealed class RegisterCommand : BaseCreateRequest<User>
{
    internal UserSettings Config { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }

    public override string ApiErrorMessagePrefix => ErrorMessages.User.RegistrationRequestPrefix;

    public void SetConfigurations(UserSettings config)
    {
        Config = config;
    }

    public override User BuildEntity()
    {
        User user = new()
        {
            Email = Email,
        };

        return user;
    }

    public override IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        int minPasswordLength = Config.MinPasswordLength;

        bool validEmail = new EmailAddressAttribute().IsValid(Email);
        bool validPasswordLength = Password.Length >= minPasswordLength;
        bool passwordsMatch = Password == RepeatPassword;

        yield return (validEmail, ErrorMessages.User.InvalidEmail);
        yield return (validPasswordLength, ErrorMessages.User.MinimumPasswordLength(minPasswordLength));
        yield return (passwordsMatch, ErrorMessages.User.PasswordMismatch);
    }
}