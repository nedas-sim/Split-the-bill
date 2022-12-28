using Application.Common;
using Domain.Responses.Groups;

namespace Application.Groups.GetInvitations;

public sealed class GetInvitationsQuery : BaseListRequest<GroupResponse>
{
    #region Auth ID
    internal Guid UserId { get; set; }
    public void SetUserId(Guid userId) => UserId = userId;
    #endregion
    #region API Params
    public string? Search { get; set; }
    #endregion
}