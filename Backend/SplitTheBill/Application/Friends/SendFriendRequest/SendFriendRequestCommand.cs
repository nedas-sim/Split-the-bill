using Domain.Common.Results;
using MediatR;

namespace Application.Friends.SendFriendRequest;

public sealed class SendFriendRequestCommand : IRequest<BaseResult<Unit>>
{
    #region API Params
    public Guid ReceivingUserId { get; set; }
    #endregion
    #region Auth ID
    internal Guid SendingUserId { get; set; }
    public void SetCallingUserId(Guid id) => SendingUserId = id;
    #endregion
}