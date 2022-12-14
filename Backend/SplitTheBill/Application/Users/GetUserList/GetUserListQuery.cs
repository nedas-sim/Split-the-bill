using Application.Common;
using Domain.Common;
using Domain.Responses.Users;

namespace Application.Users.GetUserList;

public sealed class GetUserListQuery : BaseListRequest<UserResponse>
{
    #region API Params
    public string Search { get; set; }
    #endregion
    #region Auth ID
    internal Guid CallingUserId { get; set; }
    public void SetCallingUserId(Guid id) => CallingUserId = id;
    #endregion
    #region Config
    internal UserSettings Config;
    #endregion
    #region Overrides
    public override string ApiErrorMessagePrefix() => ErrorMessages.User.GetListRequestPrefix;
    public override IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        bool validUsernameLength = Search?.Length >= Config.MinUsernameLength;

        yield return (validUsernameLength, ErrorMessages.User.MinimumSearchLength(Config.MinUsernameLength));
    }
    #endregion
}