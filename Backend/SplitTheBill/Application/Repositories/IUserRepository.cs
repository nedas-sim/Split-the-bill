﻿using Application.Friends.GetFriendList;
using Application.Friends.GetRequestList;
using Application.Friends.SendFriendRequest;
using Application.Friends.UpdateFriendRequest;
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

    public Task<int> GetUserCount(GetUserListQuery filterParams, CancellationToken cancellationToken = default);
    public Task<List<UserResponse>> GetUserList(PagingParameters pagingParameters,
                                                GetUserListQuery filterParams,
                                                CancellationToken cancellationToken = default);


    public Task<int> GetPendingFriendshipCount(GetRequestListQuery filterParams, CancellationToken cancellationToken = default);
    public Task<List<UserResponse>> GetPendingFriendshipList(PagingParameters pagingParameters,
                                                             GetRequestListQuery filterParams,
                                                             CancellationToken cancellationToken = default);

    public Task<int> GetFriendCount(GetFriendListQuery filterParams, CancellationToken cancellationToken = default);
    public Task<List<UserResponse>> GetFriendList(PagingParameters pagingParameters,
                                                  GetFriendListQuery filterParams,
                                                  CancellationToken cancellationToken = default);
}