using Application.Common;
using Domain.Common;
using Domain.Database;
using Domain.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Registration;

public sealed class RegisterCommand : BaseCreateRequest<User>
{
    internal UserSettings Config { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }

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

    public override bool IsValid(out string? errorMessage)
    {
        int minPasswordLength = Config.MinPasswordLength;

        bool validEmail = new EmailAddressAttribute().IsValid(Email);
        bool validPasswordLength = Password.Length >= minPasswordLength;
        bool passwordsMatch = Password == RepeatPassword;

        List<string> errorMessages = new();
        errorMessages.AddIfFalse(validEmail, ErrorMessages.User.InvalidEmail)
                     .AddIfFalse(validPasswordLength, ErrorMessages.User.MinimumPasswordLength(minPasswordLength))
                     .AddIfFalse(passwordsMatch, ErrorMessages.User.PasswordMismatch);

        errorMessage = errorMessages.BuildErrorMessage("Registration request has validation errors");
        return string.IsNullOrEmpty(errorMessage);
    }
}