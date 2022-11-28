using Application.Common;
using Domain.Common;
using Domain.Responses.Users;

namespace Application.Friends.GetFriendList;

public sealed class GetFriendListQuery : PagingParameters, IListRequest<UserResponse>
{
    internal Guid CallingUserId { get; set; }
    public void SetCallingUserId(Guid id) => CallingUserId = id;

    public string? Search { get; set; }

    public string ApiErrorMessagePrefix => throw new NotImplementedException();
    public IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        throw new NotImplementedException();
    }
}