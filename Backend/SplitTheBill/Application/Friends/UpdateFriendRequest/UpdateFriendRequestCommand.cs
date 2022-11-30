using Application.Common;
using MediatR;

namespace Application.Friends.UpdateFriendRequest;

public sealed class UpdateFriendRequestCommand : IResultRequest<Unit>
{
    #region API Params
    public Guid SenderId { get; set; }
    public bool IsAccepted { get; set; }
    #endregion
    #region Auth ID
    internal Guid ReceiverId { get; set; }
    public void SetReceiverId(Guid id) => ReceiverId = id;
    #endregion
}