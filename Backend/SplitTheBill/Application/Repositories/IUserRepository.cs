using Application.Friends.GetFriendList;
using Application.Friends.GetRequestList;
using Application.Friends.SendFriendRequest;
using Application.Friends.UpdateFriendRequest;
using Application.Groups.GetFriendsForGroup;
using Application.Users.GetUserList;
using Domain.Common;
using Domain.Database;
using Domain.Responses.Users;

namespace Application.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    public Task<UserResponse?> GetUserResponse(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> EmailExists(string email, CancellationToken cancellationToken = default);
    public Task<User?> GetByEmail(string email, CancellationToken cancellationToken = default);
    public Task<bool> UsernameExists(string username, CancellationToken cancellationToken = default);
    public Task<UserFriendship?> GetFriendship(SendFriendRequestCommand request, CancellationToken cancellationToken = default);
    public Task<UserFriendship?> GetFriendship(UpdateFriendRequestCommand request, CancellationToken cancellationToken = default);
    public Task PostFriendRequest(SendFriendRequestCommand request, CancellationToken cancellationToken = default);
    public Task AcceptFriendRequest(UserFriendship userFriendship, CancellationToken cancellationToken = default);
    public Task DeleteFriendRequest(UserFriendship userFriendship, CancellationToken cancellationToken = default);
    public Task<bool> ConfirmFriendship(Guid firstUserId, Guid secondUserId, CancellationToken cancellationToken = default);

    public Task<int> GetUserCount(GetUserListQuery filterParams, CancellationToken cancellationToken = default);
    public Task<List<UserResponse>> GetUserList
        (IPaging pagingParameters,
         GetUserListQuery filterParams,
         CancellationToken cancellationToken = default);


    public Task<int> GetPendingFriendshipCount(GetRequestListQuery filterParams, CancellationToken cancellationToken = default);
    public Task<List<UserResponse>> GetPendingFriendshipList
        (IPaging pagingParameters,
         GetRequestListQuery filterParams,
         CancellationToken cancellationToken = default);

    public Task<int> GetFriendCount(GetFriendListQuery filterParams, CancellationToken cancellationToken = default);
    public Task<List<UserResponse>> GetFriendList
        (IPaging pagingParameters,
         GetFriendListQuery filterParams,
         CancellationToken cancellationToken = default);

    public Task<int> GetFriendSuggestionsForGroupCount(GetFriendsForGroupQuery request, CancellationToken cancellationToken = default);
    public Task<List<UserResponse>> GetFriendSuggestionsForGroup
        (GetFriendsForGroupQuery request,
         CancellationToken cancellationToken = default);
}