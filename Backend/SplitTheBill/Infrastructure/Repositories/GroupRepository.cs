using Application.Groups.GetGroupsForFriend;
using Application.Groups.GetInvitations;
using Application.Groups.GetUsersGroupList;
using Application.Groups.SendInvitation;
using Application.Repositories;
using Domain.Common;
using Domain.Database;
using Domain.Responses.Groups;
using Domain.Views;
using Infrastructure.Database;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories;

public sealed class GroupRepository : BaseRepository<Group>, IGroupRepository
{
    #region Private Methods
    internal IQueryable<GroupMembershipView> QueryGroupMembershipViewsByFilter(GetUsersGroupListQuery query)
    {
        return
            context.GroupMembershipViews
                   .Where(gm => gm.UserId == query.UserId)
                   .Where(gm => gm.GroupName.Contains(query.Search ?? string.Empty));
    }

    internal IQueryable<UserGroup> QueryUserGroupsByFilter(GetInvitationsQuery request)
    {
        return
            context.UserGroups
                   .AsNoTracking()
                   .Where(ug => ug.AcceptedOn == null)
                   .Where(ug => ug.UserId == request.UserId)
                   .Where(ug => ug.Group.Name.Contains(request.Search ?? string.Empty));
    }
    #endregion

    public GroupRepository(DataContext context, IOptions<ConnectionStrings> options) 
        : base(context, options.Value.DefaultConnection)
    {
    }

    public async Task<List<GroupResponse>> GetUsersGroups(IPaging pagingParameters, GetUsersGroupListQuery query, CancellationToken cancellationToken = default)
    {
        List<GroupResponse> userGroups = await
            QueryGroupMembershipViewsByFilter(query)
                .AsNoTracking()
                .OrderBy(gm => gm.AcceptedOn)
                .ApplyPaging(pagingParameters)
                .Select(gm => new GroupResponse
                {
                    GroupId = gm.GroupId,
                    GroupName = gm.GroupName,
                })
                .ToListAsync(cancellationToken);

        return userGroups;
    }

    public async Task<int> UserGroupsCount(GetUsersGroupListQuery query, CancellationToken cancellationToken = default)
    {
        int totalCount = await
            QueryGroupMembershipViewsByFilter(query)
                .AsNoTracking()
                .CountAsync(cancellationToken);

        return totalCount;
    }

    public async Task<bool> IsUserAMember(Guid userId, Guid groupId, CancellationToken cancellationToken = default)
    {
        bool userIsMember = await
            context.GroupMembershipViews
                   .Where(gmv => gmv.UserId == userId)
                   .Where(gmv => gmv.GroupId == groupId)
                   .AnyAsync(cancellationToken);

        return userIsMember;
    }

    public async Task<UserGroup?> GetUserMembership(Guid userId, Guid groupId, CancellationToken cancellationToken = default)
    {
        UserGroup? userGroup = await
            context.UserGroups
                   .Where(ug => ug.UserId == userId)
                   .Where(ug => ug.GroupId == groupId)
                   .FirstOrDefaultAsync(cancellationToken);

        return userGroup;
    }

    public async Task SendGroupInvitation(SendInvitationCommand request, CancellationToken cancellationToken = default)
    {
        UserGroup userGroup = new()
        {
            GroupId = request.GroupId,
            UserId = request.ReceivingUserId,
            InvitedOn = DateTime.UtcNow,
        };

        context.UserGroups.Add(userGroup);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<GroupResponse>> GetPotentialGroupsForFriend(GetGroupsForFriendQuery request, CancellationToken cancellationToken = default)
    {
        IPaging paging = request;

        string sql = $@"
SELECT MyGroups.GroupId, GroupName
FROM (
SELECT GroupId, GroupName
FROM GroupMembershipView AS gmv
WHERE gmv.UserId = @callingUserId
) as MyGroups
LEFT JOIN UserGroups AS ug
ON MyGroups.GroupId = ug.GroupId AND @friendId = ug.UserId
WHERE UserId IS NULL AND ((@search LIKE N'') OR (CHARINDEX(@search, GroupName) > 0))
ORDER BY GroupName
OFFSET {paging.Skip} ROWS FETCH NEXT {paging.Take} ROWS ONLY";

        Dictionary<string, object> parameters = new()
        {
            { "callingUserId", request.UserId },
            { "search", request.Search ?? string.Empty },
            { "friendId", request.FriendId },
        };

        IEnumerable<GroupResponse> responses = await QueryList<GroupResponse>(sql, parameters);
        return responses.ToList();
    }

    public async Task<int> GetPotentialGroupsForFriendCount(GetGroupsForFriendQuery request, CancellationToken cancellationToken = default)
    {
        string sql = $@"
SELECT COUNT(*)
FROM (
SELECT GroupId, GroupName
FROM GroupMembershipView AS gmv
WHERE gmv.UserId = @callingUserId
) as MyGroups
LEFT JOIN UserGroups AS ug
ON MyGroups.GroupId = ug.GroupId AND @friendId = ug.UserId
WHERE UserId IS NULL AND ((@search LIKE N'') OR (CHARINDEX(@search, GroupName) > 0))";

        Dictionary<string, object> parameters = new()
        {
            { "callingUserId", request.UserId },
            { "search", request.Search ?? string.Empty },
            { "friendId", request.FriendId },
        };

        int count = await QueryValue<int>(sql, parameters);
        return count;
    }

    public async Task<List<GroupResponse>> GetInvitations(GetInvitationsQuery request, CancellationToken cancellationToken = default)
    {
        List<GroupResponse> invitations = await
            QueryUserGroupsByFilter(request)
                .OrderBy(ug => ug.Group.Name)
                .ApplyPaging(request)
                .Select(ug => new GroupResponse
                {
                    GroupId = ug.GroupId,
                    GroupName = ug.Group.Name,
                })
                .ToListAsync(cancellationToken);

        return invitations;
    }

    public async Task<int> GetInvitationCount(GetInvitationsQuery request, CancellationToken cancellationToken = default)
    {
        int count = await
            QueryUserGroupsByFilter(request)
                .CountAsync(cancellationToken);

        return count;
    }

    public async Task AcceptInvitation(UserGroup userGroup, CancellationToken cancellationToken = default)
    {
        userGroup.AcceptedOn = DateTime.UtcNow;
        context.UserGroups.Update(userGroup);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task RejectInvitation(UserGroup userGroup, CancellationToken cancellationToken = default)
    {
        context.UserGroups.Remove(userGroup);
        await context.SaveChangesAsync(cancellationToken);
    }
}