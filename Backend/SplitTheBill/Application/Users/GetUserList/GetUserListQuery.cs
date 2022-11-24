using Application.Common;
using Domain.Common;
using Domain.Responses.Users;

namespace Application.Users.GetUserList;

public sealed class GetUserListQuery : BaseValidation, IPaging, IListRequest<UserResponse>
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 20;

    internal Guid CallingUserId { get; set; }
    public void SetCallingUserId(Guid id) => CallingUserId = id;

    public string Search { get; set; }

    public override string ApiErrorMessagePrefix => ErrorMessages.User.GetListRequestPrefix;

    internal UserSettings Config;

    public override IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        bool validUsernameLength = Search?.Length >= Config.MinUsernameLength;

        yield return (validUsernameLength, ErrorMessages.User.MinimumSearchLength(Config.MinUsernameLength));
    }
}