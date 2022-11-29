using Application.Common;
using Domain.Responses.Users;

namespace Application.Friends.GetFriendList;

public sealed class GetFriendListQuery : BaseListRequest<UserResponse>
{
    #region API Params
    public string? Search { get; set; }
    #endregion
    #region Auth ID
    internal Guid CallingUserId { get; set; }
    public void SetCallingUserId(Guid id) => CallingUserId = id;
    #endregion
}