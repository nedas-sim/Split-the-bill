using Application.Common;
using Domain.Common;
using Domain.Responses.Users;

namespace Application.Friends.GetRequestList;

public sealed class GetRequestListQuery : PagingParameters, IListRequest<UserResponse>
{
    public string? Search { get; set; }

    internal Guid CallingUserId { get; set; }
    public void SetCallingUserId(Guid id) => CallingUserId = id;

    public string ApiErrorMessagePrefix => throw new NotImplementedException();
    public IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        throw new NotImplementedException();
    }
}