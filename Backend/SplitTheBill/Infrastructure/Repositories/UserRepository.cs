using Application.Friends.GetFriendList;
using Application.Friends.GetRequestList;
using Application.Friends.SendFriendRequest;
using Application.Friends.UpdateFriendRequest;
using Application.Groups.GetFriendsForGroup;
using Application.Repositories;
using Application.Users.GetUserList;
using Domain.Common;
using Domain.Database;
using Domain.Responses.Users;
using Domain.Views;
using Infrastructure.Database;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories;

public sealed class UserRepository : BaseRepository<User>, IUserRepository
{
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

    internal IQueryable<PendingFriendshipView> QueryByFilter(GetRequestListQuery filterParams)
    {
        IQueryable<PendingFriendshipView> queryByFilter =
            context.PendingFriendshipViews
                   .Where(pf => pf.RequestReceiverId == filterParams.CallingUserId)
                   .Where(pf => pf.SenderUsername.Contains(filterParams.Search ?? string.Empty) ||
                                pf.SenderEmail == filterParams.Search);

        return queryByFilter;
    }

    internal async Task<UserFriendship?> GetFriendship(Guid senderId, Guid receiverId, CancellationToken cancellationToken)
    {
        UserFriendship? friendship = await
            context.UserFriendships
                   .Where(uf => uf.RequestSenderId == senderId || uf.RequestReceiverId == senderId)
                   .Where(uf => uf.RequestSenderId == receiverId || uf.RequestReceiverId == receiverId)
                   .FirstOrDefaultAsync(cancellationToken);

        return friendship;
    }
    #endregion

    public UserRepository(DataContext context, IOptions<ConnectionStrings> options) 
        : base(context, options.Value.DefaultConnection)
    {
    }

    public async Task AcceptFriendRequest(UserFriendship userFriendship, CancellationToken cancellationToken = default)
    {
        userFriendship.AcceptedOn = DateTime.UtcNow;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteFriendRequest(UserFriendship userFriendship, CancellationToken cancellationToken = default)
    {
        context.UserFriendships.Remove(userFriendship);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetPendingFriendshipCount(GetRequestListQuery filterParams, CancellationToken cancellationToken = default)
    {
        int totalCount =
            await QueryByFilter(filterParams)
                .AsNoTracking()
                .CountAsync(cancellationToken);

        return totalCount;
    }

    public async Task<List<UserResponse>> GetPendingFriendshipList
        (IPaging pagingParameters, 
         GetRequestListQuery filterParams, 
         CancellationToken cancellationToken = default)
    {
        List<UserResponse> userResponses = await
            QueryByFilter(filterParams)
                .AsNoTracking()
                .OrderBy(pf => pf.SenderUsername)
                .ApplyPaging(pagingParameters)
                .Select(pf => new UserResponse
                {
                    Id = pf.RequestSenderId,
                    Username = pf.SenderUsername,
                    UserSentTheRequest = true, // default values for API response to work
                    InvitedOn = DateTime.UtcNow,
                })
                .ToListAsync(cancellationToken);

        return userResponses;
    }

    public async Task<UserFriendship?> GetFriendship(UpdateFriendRequestCommand request, CancellationToken cancellationToken = default)
    {
        return await GetFriendship(request.SenderId, request.ReceiverId, cancellationToken);
    }

    public async Task<UserFriendship?> GetFriendship(SendFriendRequestCommand request, CancellationToken cancellationToken = default)
    {
        return await GetFriendship(request.SendingUserId, request.ReceivingUserId, cancellationToken);
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

    public async Task<List<UserResponse>> GetUserList(IPaging pagingParameters, GetUserListQuery filterParams, CancellationToken cancellationToken = default)
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
      SELECT [u].[Id], [u].[Username], [uf].[InvitedOn], [uf].[AcceptedOn], 
      CASE
        WHEN [u].[Id] = [uf].[RequestSenderId] THEN 1
        ELSE 0
      END AS UserSentTheRequest
	  
      FROM [Users] AS [u]

	  LEFT JOIN [UserFriendships] AS [uf]
		ON ([uf].[RequestReceiverId] = @callingUserId OR [uf].[RequestSenderId] = @callingUserId)
       AND ([uf].[RequestReceiverId] = [u].[Id] OR [uf].[RequestSenderId] = [u].[Id])

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

    public async Task<int> GetFriendCount(GetFriendListQuery filterParams, CancellationToken cancellationToken = default)
    {
        string sql = $@"
SELECT COUNT(*)
FROM (
SELECT afw.RequestReceiverId AS Id, afw.ReceiverUsername AS Username, afw.AcceptedOn, afw.AcceptedOn AS InvitedOn, afw.ReceiverEmail AS Email
FROM AcceptedFriendshipView AS afw
WHERE afw.RequestSenderId = @callingUserId

UNION ALL
SELECT afw.RequestSenderId AS Id, afw.SenderUsername AS Username, afw.AcceptedOn, afw.AcceptedOn AS InvitedOn, afw.SenderEmail AS Email
FROM AcceptedFriendshipView AS afw
WHERE afw.RequestReceiverId = @callingUserId
) AS u

WHERE (@search LIKE N'') OR (CHARINDEX(@search, [u].[Username]) > 0) OR ([u].[Email] = @search)";

        Dictionary<string, object> parameters = new()
        {
            { "callingUserId", filterParams.CallingUserId },
            { "search", filterParams.Search ?? string.Empty },
        };

        return await QueryValue<int>(sql, parameters);
    }

    public async Task<List<UserResponse>> GetFriendList(IPaging pagingParameters, GetFriendListQuery filterParams, CancellationToken cancellationToken = default)
    {
        string sql = $@"
SELECT [u].[Id], [u].[Username], [u].[InvitedOn], [u].[AcceptedOn], 1 AS UserSentTheRequest
FROM (
SELECT afw.RequestReceiverId AS Id, afw.ReceiverUsername AS Username, afw.AcceptedOn, afw.AcceptedOn AS InvitedOn, afw.ReceiverEmail AS Email
FROM AcceptedFriendshipView AS afw
WHERE afw.RequestSenderId = @callingUserId

UNION ALL
SELECT afw.RequestSenderId AS Id, afw.SenderUsername AS Username, afw.AcceptedOn, afw.AcceptedOn AS InvitedOn, afw.SenderEmail AS Email
FROM AcceptedFriendshipView AS afw
WHERE afw.RequestReceiverId = @callingUserId
) AS u

WHERE (@search LIKE N'') OR (CHARINDEX(@search, [u].[Username]) > 0) OR ([u].[Email] = @search)

ORDER BY [u].[Username]
OFFSET {pagingParameters.Skip} ROWS FETCH NEXT {pagingParameters.Take} ROWS ONLY";

        Dictionary<string, object> parameters = new()
        {
            { "callingUserId", filterParams.CallingUserId },
            { "search", filterParams.Search ?? string.Empty },
        };

        IEnumerable<UserResponse> userResponses = await QueryList<UserResponse>(sql, parameters);
        return userResponses.ToList();
    }

    public async Task<bool> ConfirmFriendship(Guid firstUserId, Guid secondUserId, CancellationToken cancellationToken = default)
    {
        bool usersAreFriends = await
            context.AcceptedFriendshipViews
                   .Where(afv => afv.RequestSenderId == firstUserId || afv.RequestReceiverId == firstUserId)
                   .Where(afv => afv.RequestSenderId == secondUserId || afv.RequestReceiverId == secondUserId)
                   .AnyAsync(cancellationToken);

        return usersAreFriends;
    }

    public async Task<int> GetFriendSuggestionsForGroupCount(GetFriendsForGroupQuery request, CancellationToken cancellationToken = default)
    {
        // reiks rasyt raw sql

        /*
         DECLARE @callingUserId AS UNIQUEIDENTIFIER SET @callingUserId = 'eb733b22-b2ca-4e56-aa3f-08daae8ee83a'
DECLARE @groupId AS UNIQUEIDENTIFIER SET @groupId = 'e061ab1c-7d19-4821-a75d-08dab39babf2'
DECLARE @search AS NVARCHAR SET @search = ''

SELECT *
FROM (
SELECT *
FROM ( SELECT UserId FROM UserGroups ug WHERE ug.GroupId = @groupId ) AS ug
RIGHT JOIN (
-- SELECTING FRIENDS
SELECT afw.RequestReceiverId AS FriendId, afw.ReceiverUsername AS Username, afw.AcceptedOn, afw.AcceptedOn AS InvitedOn, afw.ReceiverEmail AS Email
FROM AcceptedFriendshipView AS afw
WHERE afw.RequestSenderId = @callingUserId

UNION ALL
SELECT afw.RequestSenderId AS FriendId, afw.SenderUsername AS Username, afw.AcceptedOn, afw.AcceptedOn AS InvitedOn, afw.SenderEmail AS Email
FROM AcceptedFriendshipView AS afw
WHERE afw.RequestReceiverId = @callingUserId
-- END SELECTING FRIENDS
) AS Friends ON ug.UserId = Friends.FriendId
) AS FriendStatusForGroup
WHERE FriendStatusForGroup.UserId IS NULL
         */
        throw new NotImplementedException();
    }

    public Task<List<UserResponse>> GetFriendSuggestionsForGroup(GetFriendsForGroupQuery request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}