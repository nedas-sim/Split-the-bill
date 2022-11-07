using Application.Common;
using MediatR;

namespace Application.Friends.UpdateFriendRequest;

public sealed class UpdateFriendRequestCommand : IResultRequest<Unit>
{
    public Guid SenderId { get; set; }
    public bool IsAccepted { get; set; }

    internal Guid ReceiverId { get; set; }
    public void SetReceiverId(Guid id) => ReceiverId = id;
}