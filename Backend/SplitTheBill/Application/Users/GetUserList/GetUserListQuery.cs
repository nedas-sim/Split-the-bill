using Application.Common;
using Domain.Common;
using Domain.Extensions;
using Domain.Responses.Users;

namespace Application.Users.GetUserList;

public sealed class GetUserListQuery : PagingParameters, IListRequest<UserResponse>
{
    public static string UsernameLengthErrorMessage(int length) => $"Username has to contain at least {length} characters";

    internal Guid CallingUserId { get; set; }
    public void SetCallingUserId(Guid id) => CallingUserId = id;

    public string Username { get; set; }

    private UserSettings _config;
    public void SetConfigurations(UserSettings config)
    {
        _config = config;
    }

    public bool IsValid(out string? errorMessage)
    {
        List<string> errorMessages = new();
        bool validUsernameLength = Username?.Length >= _config.MinUsernameLength;
        errorMessages.AddIfFalse(validUsernameLength, UsernameLengthErrorMessage(_config.MinUsernameLength));
        errorMessage = errorMessages.BuildErrorMessage("Get user list request has validation errors");
        return string.IsNullOrEmpty(errorMessage);
    }
}