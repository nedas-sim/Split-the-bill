using Application.Common;
using Domain.Responses.Users;

namespace Application.Groups.GetFriendsForGroup;

public sealed class GetFriendsForGroupQuery : BaseListRequest<UserResponse>
{
    #region Auth ID
    internal Guid UserId { get; set; }
    public void SetUserId(Guid userId) => UserId = userId;
    #endregion
    #region API Params
    public string? Search { get; set; }
    public Guid GroupId { get; set; }
    #endregion
}