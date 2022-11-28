using Application.Common;
using Domain.Responses.Groups;

namespace Application.Groups.GetUsersGroupList;

public sealed class GetUsersGroupListQuery : BaseListRequest<GroupResponse>
{
    internal Guid UserId { get; set; }
    public void SetUserId(Guid userId) => UserId = userId;
}