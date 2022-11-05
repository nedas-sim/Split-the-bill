using Application.Repositories;
using Domain.Common;
using Domain.Database;
using Domain.Responses.Groups;
using Domain.Views;
using Infrastructure.Database;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class GroupRepository : BaseRepository<Group>, IGroupRepository
{
    public GroupRepository(DataContext context) : base(context)
    {
    }

    public async Task<List<GroupResponse>> GetUsersGroups(PagingParameters pagingParameters, Guid userId, CancellationToken cancellationToken = default)
    {
        List<GroupResponse> userGroups = await
            QueryGroupMembershipViewsByUserId(userId)
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

    public async Task<int> UserGroupsCount(Guid userId, CancellationToken cancellationToken = default)
    {
        int totalCount = await
            QueryGroupMembershipViewsByUserId(userId)
                .AsNoTracking()
                .CountAsync(cancellationToken);

        return totalCount;
    }

    #region Private Methods
    private IQueryable<GroupMembershipView> QueryGroupMembershipViewsByUserId(Guid userId)
    {
        return 
            context.GroupMembershipViews
                   .Where(gm => gm.UserId == userId);
    }
    #endregion
}