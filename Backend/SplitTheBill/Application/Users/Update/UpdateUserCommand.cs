using Application.Common;
using Domain.Common;
using Domain.Database;
using Domain.Extensions;
using Domain.Responses.Users;
using System.ComponentModel.DataAnnotations;

namespace Application.Users.Update;

public sealed class UpdateUserCommand : BaseUpdateRequest<User, UserResponse>
{
    public static string UsernameLengthErrorMessage(int length) => $"Username has to contain at least {length} characters";

    public string? Username { get; set; }
    public string? Email { get; set; }

    internal UserSettings Config { get; set; }

    public override bool IsValid(out string? errorMessage)
    {
        List<string> errorMessages = new();

        bool validEmail = new EmailAddressAttribute().IsValid(Email);
        bool validUsernameLength = Username is null || Username.Length >= Config.MinUsernameLength;

        errorMessages.AddIfFalse(validEmail, "Invalid email address");
        errorMessages.AddIfFalse(validUsernameLength, UsernameLengthErrorMessage(Config.MinUsernameLength));

        errorMessage = errorMessages.BuildErrorMessage("Update user profile request has validation errors");

        return string.IsNullOrEmpty(errorMessage);
    }

    public override void Update(User databaseEntity)
    {
        databaseEntity.Username = Username ?? databaseEntity.Username;
        databaseEntity.Email = Email ?? databaseEntity.Email;
    }
}