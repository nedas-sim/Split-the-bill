using Application.Common;
using Domain.Responses.Users;

namespace Application.Friends.GetRequestList;

public sealed class GetRequestListQuery : BaseListRequest<UserResponse>
{
    #region API Params
    public string? Search { get; set; }
    #endregion
    #region Auth ID
    internal Guid CallingUserId { get; set; }
    public void SetCallingUserId(Guid id) => CallingUserId = id;
    #endregion
}