using Application.Common;
using Domain.Common;
using Domain.Responses.Users;

namespace Application.Users.GetUserList;

public sealed class GetUserListQuery : BaseListRequest<UserResponse>
{
    internal Guid CallingUserId { get; set; }
    public void SetCallingUserId(Guid id) => CallingUserId = id;

    public string Search { get; set; }

    internal UserSettings Config;

    public override string ApiErrorMessagePrefix => ErrorMessages.User.GetListRequestPrefix;
    public override IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        bool validUsernameLength = Search?.Length >= Config.MinUsernameLength;

        yield return (validUsernameLength, ErrorMessages.User.MinimumSearchLength(Config.MinUsernameLength));
    }
}