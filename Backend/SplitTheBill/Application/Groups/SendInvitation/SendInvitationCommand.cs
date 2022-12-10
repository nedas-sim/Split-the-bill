using Application.Common;
using MediatR;

namespace Application.Groups.SendInvitation;

public sealed class SendInvitationCommand : IResultRequest<Unit>
{
    #region API Params
    public Guid ReceivingUserId { get; set; }
    public Guid GroupId { get; set; }
    #endregion
    #region Auth ID
    internal Guid SendingUserId { get; set; }
    public void SetCallingUserId(Guid id) => SendingUserId = id;
    #endregion
}
