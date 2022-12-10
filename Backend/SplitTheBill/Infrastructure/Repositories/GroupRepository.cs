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
                    MemberCount = context.GroupMembershipViews
                                         .Where(gmv => gmv.GroupId == gm.GroupId)
                                         .Count(),
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
}