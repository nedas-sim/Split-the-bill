using Application.Common;
using Domain.Common;
using Domain.Extensions;
using Domain.Responses.Users;

namespace Application.Users.GetUserList;

public sealed class GetUserListQuery : PagingParameters, IListRequest<UserResponse>
{
    internal Guid CallingUserId { get; set; }
    public void SetCallingUserId(Guid id) => CallingUserId = id;

    public string Search { get; set; }

    internal UserSettings Config;

    public bool IsValid(out string? errorMessage)
    {
        List<string> errorMessages = new();
        bool validUsernameLength = Search?.Length >= Config.MinUsernameLength;
        errorMessages.AddIfFalse(validUsernameLength, ErrorMessages.User.MinimumSearchLength(Config.MinUsernameLength));
        errorMessage = errorMessages.BuildErrorMessage(ErrorMessages.User.GetListRequestPrefix);
        return string.IsNullOrEmpty(errorMessage);
    }
}