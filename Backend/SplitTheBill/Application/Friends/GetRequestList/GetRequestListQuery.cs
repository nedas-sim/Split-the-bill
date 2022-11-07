using Application.Common;
using Domain.Common;
using Domain.Extensions;
using Domain.Responses.Users;

namespace Application.Friends.GetRequestList;

public sealed class GetRequestListQuery : PagingParameters, IListRequest<UserResponse>
{
    public static string SearchLengthErrorMessage(int length) => $"Search value has to contain at least {length} characters";

    public string Search { get; set; }

    internal Guid CallingUserId { get; set; }
    public void SetCallingUserId(Guid id) => CallingUserId = id;

    private UserSettings _config;
    public void SetConfigurations(UserSettings config)
    {
        _config = config;
    }

    public bool IsValid(out string? errorMessage)
    {
        List<string> errorMessages = new();
        bool validUsernameLength = Search?.Length >= _config.MinUsernameLength;
        errorMessages.AddIfFalse(validUsernameLength, SearchLengthErrorMessage(_config.MinUsernameLength));
        errorMessage = errorMessages.BuildErrorMessage("Get pending invitation list request has validation errors");
        return string.IsNullOrEmpty(errorMessage);
    }
}