using Application.Common;
using Domain.Database;
using Domain.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Application.Users.Update;

public sealed class UpdateUserCommand : BaseUpdateRequest<User>
{
    public string? Username { get; set; }
    public string? Email { get; set; }

    public override bool IsValid(out string? errorMessage)
    {
        List<string> errorMessages = new();

        bool validEmail = new EmailAddressAttribute().IsValid(Email);

        errorMessages.AddIfFalse(validEmail, "Invalid email address");

        errorMessage = errorMessages.BuildErrorMessage("Update user profile request has validation errors");

        return string.IsNullOrEmpty(errorMessage);
    }

    public override void Update(User databaseEntity)
    {
        databaseEntity.Username = Username ?? databaseEntity.Username;
        databaseEntity.Email = Email ?? databaseEntity.Email;
    }
}