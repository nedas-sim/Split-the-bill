using Application.Friends.SendFriendRequest;
using Application.Users.GetUserList;
using Domain.Common;
using Domain.Database;
using Domain.Enums;
using Domain.Responses.Users;

namespace Application.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    public Task<UserResponse?> GetUserResponse(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> EmailExists(string email, CancellationToken cancellationToken = default);
    public Task<User?> GetByEmail(string email, CancellationToken cancellationToken = default);
    public Task<bool> UsernameExists(string username, CancellationToken cancellationToken = default);
    public Task<int> GetUserCount(GetUserListQuery filterParams, CancellationToken cancellationToken = default);
    public Task<FriendshipStatus> GetFriendshipStatus(SendFriendRequestCommand request, CancellationToken cancellationToken = default);
    public Task PostFriendRequest(SendFriendRequestCommand request, CancellationToken cancellationToken = default);
    public Task<List<UserResponse>> GetUserList(PagingParameters pagingParameters,
                                                GetUserListQuery filterParams,
                                                CancellationToken cancellationToken = default);
}