using Application.Common;
using Domain.Responses.Users;

namespace Application.Friends.GetRequestList;

public sealed class GetRequestListQuery : BaseListRequest<UserResponse>
{
    public string? Search { get; set; }

    internal Guid CallingUserId { get; set; }
    public void SetCallingUserId(Guid id) => CallingUserId = id;
}