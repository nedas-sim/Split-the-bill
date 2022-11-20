using Application.Common;
using Domain.Common;
using Domain.Responses.Users;

namespace Application.Friends.GetRequestList;

public sealed class GetRequestListQuery : PagingParameters, IListRequest<UserResponse>
{
    public static string SearchLengthErrorMessage(int length) => $"Search value has to contain at least {length} characters";

    public string? Search { get; set; }

    internal Guid CallingUserId { get; set; }
    public void SetCallingUserId(Guid id) => CallingUserId = id;
}