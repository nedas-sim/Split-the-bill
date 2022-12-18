using Application.Common;
using Domain.Responses.Groups;

namespace Application.Groups.GetGroupsForFriend;

public sealed class GetGroupsForFriendQuery : BaseListRequest<GroupResponse>
{
    #region Auth ID
    internal Guid UserId { get; set; }
    public void SetUserId(Guid userId) => UserId = userId;
    #endregion
    #region API Params
    public string? Search { get; set; }
    public Guid FriendId { get; set; }
    #endregion
}