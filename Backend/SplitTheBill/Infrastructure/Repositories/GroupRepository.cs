using Application.Repositories;
using Domain.Database;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class GroupRepository : BaseRepository<Group>, IGroupRepository
{
    public GroupRepository(DataContext context) : base(context)
    {
    }

    public async Task<bool> GroupNameExists(string name, CancellationToken cancellationToken = default)
    {
        bool groupNameExists =
            await context.Groups
                         .Where(g => g.Name == name)
                         .AnyAsync(cancellationToken);

        return groupNameExists;
    }
}