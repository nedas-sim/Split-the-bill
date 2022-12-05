using Application.Groups.GetUsersGroupList;
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
    public GroupRepository(DataContext context, IOptions<ConnectionStrings> options) 
        : base(context, options.Value.DefaultConnection)
    {
    }

    public async Task<List<GroupResponse>> GetUsersGroups(IPaging pagingParameters, GetUsersGroupListQuery query, CancellationToken cancellationToken = default)
    {
        List<GroupResponse> userGroups = await
            QueryGroupMembershipViewsByQuery(query)
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
            QueryGroupMembershipViewsByQuery(query)
                .AsNoTracking()
                .CountAsync(cancellationToken);

        return totalCount;
    }

    #region Private Methods
    private IQueryable<GroupMembershipView> QueryGroupMembershipViewsByQuery(GetUsersGroupListQuery query)
    {
        return
            context.GroupMembershipViews
                   .Where(gm => gm.UserId == query.UserId)
                   .Where(gm => gm.GroupName.Contains(query.Search ?? string.Empty));
    }
    #endregion
}