using Application.Common;
using MediatR;

namespace Application.Groups.UpdateInvitation;

public sealed class UpdateInvitationCommand : IResultRequest<Unit>
{
    #region API Params
    public Guid GroupId { get; set; }
    public bool IsAccepted { get; set; }
    #endregion
    #region Auth ID
    internal Guid UserId { get; set; }
    public void SetUserId(Guid id) => UserId = id;
    #endregion
}