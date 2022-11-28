using Application.Common;
using Domain.Common;
using Domain.Responses.Groups;

namespace Application.Groups.GetUsersGroupList;

public sealed class GetUsersGroupListQuery : PagingParameters, IListRequest<GroupResponse>
{
    internal Guid UserId { get; set; }
    public void SetUserId(Guid userId) => UserId = userId;

    public string ApiErrorMessagePrefix => throw new NotImplementedException();
    public IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        throw new NotImplementedException();
    }
}