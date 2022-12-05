using Application.Common;
using Domain.Responses.Groups;

namespace Application.Groups.GetUsersGroupList;

public sealed class GetUsersGroupListQuery : BaseListRequest<GroupResponse>
{
    #region Auth ID
    internal Guid UserId { get; set; }
    public void SetUserId(Guid userId) => UserId = userId;
    #endregion
    #region API Params
    public string? Search { get; set; }
    #endregion
}