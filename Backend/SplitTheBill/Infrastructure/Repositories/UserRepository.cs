using Application.Friends.SendFriendRequest;
using Application.Repositories;
using Application.Users.GetUserList;
using Domain.Common;
using Domain.Database;
using Domain.Enums;
using Domain.Responses.Users;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories;

public sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DataContext context, IOptions<ConnectionStrings> options) 
        : base(context, options.Value.DefaultConnection)
    {
    }

    public async Task<FriendshipStatus> GetFriendshipStatus(SendFriendRequestCommand request, CancellationToken cancellationToken = default)
    {
        UserFriendship? friendship = 
            await GetUserFriendshipByUserIds(request.SendingUserId, 
                                             request.ReceivingUserId, 
                                             cancellationToken);

        if (friendship is null)
        {
            return FriendshipStatus.NotInvited;
        }

        return friendship.AcceptedOn switch
        {
            null => FriendshipStatus.PendingAcception,
            _ => FriendshipStatus.Friends,
        };
    }

    public async Task PostFriendRequest(SendFriendRequestCommand request, CancellationToken cancellationToken = default)
    {
        UserFriendship friendship = new()
        {
            RequestSenderId = request.SendingUserId,
            RequestReceiverId = request.ReceivingUserId,
            InvitedOn = DateTime.UtcNow,
        };

        context.UserFriendships.Add(friendship);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserResponse?> GetUserResponse(Guid id, CancellationToken cancellationToken = default)
    {
        UserResponse? user =
            await context.Users
                         .AsNoTracking()
                         .Where(u => u.Id == id)
                         .Select(u => new UserResponse
                         {
                             Id = u.Id,
                             Username = u.Username,
                             IsFriend = false, // have no user id to determine friendship, so set it to false
                         })
                         .FirstOrDefaultAsync(cancellationToken);

        return user;
    }

    public async Task<bool> EmailExists(string email, CancellationToken cancellationToken = default)
    {
        bool exists =
            await QueryByEmail(email)
                .AsNoTracking()
                .AnyAsync(cancellationToken);

        return exists;
    }

    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        User? user =
            await QueryByEmail(email)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

        return user;
    }

    public async Task<bool> UsernameExists(string username, CancellationToken cancellationToken = default)
    {
        bool exists =
            await context.Users
                         .AsNoTracking()
                         .Where(u => u.Username == username)
                         .AnyAsync(cancellationToken);

        return exists;
    }

    public async Task<int> GetUserCount(GetUserListQuery filterParams, CancellationToken cancellationToken = default)
    {
        int totalCount =
            await QueryByFilter(filterParams)
                .AsNoTracking()
                .CountAsync(cancellationToken);

        return totalCount;
    }

    public async Task<List<UserResponse>> GetUserList(PagingParameters pagingParameters, GetUserListQuery filterParams, CancellationToken cancellationToken = default)
    {
        #region EF
        /*List<UserResponse> userList =
            await QueryByFilter(filterParams)
                .AsNoTracking()
                .OrderBy(u => u.Username)
                .ApplyPaging(pagingParameters)
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    Username = u.Username,
                })
                .ToListAsync(cancellationToken);

        List<Guid> userIds = userList.Select(u => u.Id).ToList();

        List<AcceptedFriendshipView> acceptedFriendships =
            await context.AcceptedFriendshipViews
                         .AsNoTracking()
                         .Where(af => userIds.Contains(af.RequestSenderId) || userIds.Contains(af.RequestReceiverId))
                         .Where(af => af.RequestSenderId == filterParams.CallingUserId || af.RequestReceiverId == filterParams.CallingUserId)
                         .ToListAsync(cancellationToken);

        foreach (UserResponse user in userList)
        {
            bool isFriend =
                acceptedFriendships.Where(af => af.RequestSenderId == user.Id || af.RequestReceiverId == user.Id)
                                   .Any();

            user.IsFriend = isFriend;
        }

        return userList;*/
        #endregion

        string sql = $@"
      SELECT [u].[Id], [u].[Username],
	  
	  (
	  SELECT 1 
	  FROM [AcceptedFriendshipView] AS a
	  WHERE 1 = 1
		AND ([a].[RequestSenderId] = [u].[Id] OR [a].[RequestReceiverId] = [u].[Id])
		AND ([a].[RequestSenderId] = @callingUserId OR [a].[RequestReceiverId] = @callingUserId)
	  ) AS IsFriend
	  
      FROM [Users] AS [u]

      WHERE ([u].[Id] <> @callingUserId) AND 
            (((@search LIKE N'') OR (CHARINDEX(@search, [u].[Username]) > 0)) OR ([u].[Email] = @search))
      ORDER BY [u].[Username]
      OFFSET {pagingParameters.Skip} ROWS FETCH NEXT {pagingParameters.Take} ROWS ONLY";

        Dictionary<string, object> parameters = new()
        {
            { "callingUserId", filterParams.CallingUserId },
            { "search", filterParams.Search },
        };

        IEnumerable<UserResponse> userResponses = await QueryList<UserResponse>(sql, parameters);
        return userResponses.ToList();
    }

    #region Private Methods
    internal IQueryable<User> QueryByEmail(string email)
    {
        IQueryable<User> queryByEmail = 
            context.Users
                   .Where(u => u.Email == email);

        return queryByEmail;
    }

    internal IQueryable<User> QueryByFilter(GetUserListQuery filterParams)
    {
        IQueryable<User> queryByFilter =
            context.Users
                   .Where(u => u.Id != filterParams.CallingUserId)
                   .Where(u => u.Username.Contains(filterParams.Search) ||
                               u.Email == filterParams.Search);

        return queryByFilter;
    }

    internal async Task<UserFriendship?> GetUserFriendshipByUserIds(Guid senderId, Guid receiverId, CancellationToken cancellationToken)
    {
        UserFriendship? friendship = await
            context.UserFriendships
                   .Where(uf => uf.RequestSenderId == senderId)
                   .Where(uf => uf.RequestReceiverId == receiverId)
                   .FirstOrDefaultAsync(cancellationToken);

        return friendship;
    }
    #endregion
}