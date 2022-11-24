using Application.Common;
using Domain.Common;
using Domain.Database;
using Domain.Responses.Users;
using System.ComponentModel.DataAnnotations;

namespace Application.Users.Update;

public sealed class UpdateUserCommand : BaseUpdateRequest<User, UserResponse>
{
    public string? Username { get; set; }
    public string? Email { get; set; }

    public override string ApiErrorMessagePrefix => ErrorMessages.User.UpdateRequestPrefix;

    internal UserSettings Config { get; set; }

    public override IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        bool validEmail = new EmailAddressAttribute().IsValid(Email);
        bool validUsernameLength = Username is null || Username.Length >= Config.MinUsernameLength;

        yield return (validEmail, ErrorMessages.User.InvalidEmail);
        yield return (validUsernameLength, ErrorMessages.User.MinimumUsernameLength(Config.MinUsernameLength));
    }

    public override void Update(User databaseEntity)
    {
        databaseEntity.Username = Username ?? databaseEntity.Username;
        databaseEntity.Email = Email ?? databaseEntity.Email;
    }
}