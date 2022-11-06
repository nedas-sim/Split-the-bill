using Domain.Common.Results;
using MediatR;

namespace Application.Friends.SendFriendRequest;

public sealed class SendFriendRequestCommand : IRequest<BaseResult<Unit>>
{
    public Guid ReceivingUserId { get; set; }

    internal Guid SendingUserId { get; set; }
    public void SetCallingUserId(Guid id) => SendingUserId = id;
}