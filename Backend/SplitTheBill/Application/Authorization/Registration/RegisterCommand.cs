using Application.Common;
using Domain.Common;
using Domain.Database;
using Domain.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Registration;

public sealed class RegisterCommand : BaseCreateRequest<User>
{
    private UserSettings _config;

    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }

    public void SetConfigurations(UserSettings config)
    {
        _config = config;
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
        int minPasswordLength = _config.MinPasswordLength;

        bool validEmail = new EmailAddressAttribute().IsValid(Email);
        bool validPasswordLength = Password.Length >= minPasswordLength;
        bool passwordsMatch = Password == RepeatPassword;

        List<string> errorMessages = new();
        errorMessages.AddIfFalse(validEmail, "Invalid email address")
                     .AddIfFalse(validPasswordLength, $"Password has to contain at least {minPasswordLength} characters")
                     .AddIfFalse(passwordsMatch, "Passwords do not match");

        errorMessage = errorMessages.BuildErrorMessage("Registration request has validation errors");
        return string.IsNullOrEmpty(errorMessage);
    }
}