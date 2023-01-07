using Application.Finances.GetGroupEntries;
using Application.Repositories;
using Domain.Common;
using Domain.Database;
using Domain.Responses.Finances;
using Infrastructure.Database;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories;

public sealed class EntryRepository : BaseRepository<Entry>, IEntryRepository
{
    #region Private Methods
    internal IQueryable<Entry> QueryByFilter(GetGroupEntriesQuery request)
    {
        return context.Entries
                      .AsNoTracking()
                      .Where(e => e.GroupId == request.GroupId)
                      .Where(e => e.Name.Contains(request.Search ?? string.Empty));
    }
    #endregion

    public EntryRepository(DataContext context, IOptions<ConnectionStrings> options)
        : base(context, options.Value.DefaultConnection)
    {
    }

    public async Task<List<EntryResponse>> GetGroupEntries(GetGroupEntriesQuery request, CancellationToken cancellationToken)
    {
        List<EntryResponse> entryResponses = await
            QueryByFilter(request)
                .OrderByDescending(e => e.CreatedOn)
                .ApplyPaging(request)
                .Select(e => new EntryResponse
                {
                    Name = e.Name,
                    Description = e.Description,
                    EntryId = e.Id,
                })
                .ToListAsync(cancellationToken);

        return entryResponses;
    }

    public async Task<int> GetGroupEntryCount(GetGroupEntriesQuery request, CancellationToken cancellationToken)
    {
        int entryCount = await
            QueryByFilter(request)
                .CountAsync(cancellationToken);

        return entryCount;
    }
}