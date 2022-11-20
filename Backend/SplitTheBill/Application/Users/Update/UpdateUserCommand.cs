using Application.Common;
using Domain.Common;
using Domain.Database;
using Domain.Extensions;
using Domain.Responses.Users;
using System.ComponentModel.DataAnnotations;

namespace Application.Users.Update;

public sealed class UpdateUserCommand : BaseUpdateRequest<User, UserResponse>
{
    public string? Username { get; set; }
    public string? Email { get; set; }

    internal UserSettings Config { get; set; }

    public override bool IsValid(out string? errorMessage)
    {
        List<string> errorMessages = new();

        bool validEmail = new EmailAddressAttribute().IsValid(Email);
        bool validUsernameLength = Username is null || Username.Length >= Config.MinUsernameLength;

        errorMessages.AddIfFalse(validEmail, ErrorMessages.User.InvalidEmail);
        errorMessages.AddIfFalse(validUsernameLength, ErrorMessages.User.MinimumUsernameLength(Config.MinUsernameLength));

        errorMessage = errorMessages.BuildErrorMessage(ErrorMessages.User.UpdateRequestPrefix);

        return string.IsNullOrEmpty(errorMessage);
    }

    public override void Update(User databaseEntity)
    {
        databaseEntity.Username = Username ?? databaseEntity.Username;
        databaseEntity.Email = Email ?? databaseEntity.Email;
    }
}