using Application.Common;
using Domain.Responses.Users;

namespace Application.Friends.GetFriendList;

public sealed class GetFriendListQuery : BaseListRequest<UserResponse>
{
    internal Guid CallingUserId { get; set; }
    public void SetCallingUserId(Guid id) => CallingUserId = id;

    public string? Search { get; set; }
}