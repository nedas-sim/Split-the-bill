using Application.Finances.GetEntryExpenses;
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

    public async Task<int> GetEntryExpenseCount(GetEntryExpensesQuery request, CancellationToken cancellationToken)
    {
        string sql = $@"
SELECT COUNT(*)
FROM (
SELECT DebtorId AS FriendId, EntryId
FROM EntryExpenses AS ee
WHERE ee.PayerId = @UserId

UNION ALL
SELECT PayerId AS FriendId, EntryId
FROM EntryExpenses AS ee
WHERE ee.DebtorId = @UserId) AS MyFinLines

INNER JOIN Entries AS e ON MyFinLines.EntryId = e.Id

WHERE (@FriendId IS NULL OR MyFinLines.FriendId = @FriendId) AND e.GroupId = @GroupId";

        Dictionary<string, object?> parameters = new()
        {
            { "UserId", request.UserId },
            { "FriendId", request.FriendId },
            { "GroupId", request.GroupId },
        };

        return await QueryValue<int>(sql, parameters);
    }

    public async Task<List<EntryExpenseResponse>> GetEntryExpenses(GetEntryExpensesQuery request, CancellationToken cancellationToken)
    {
        IPaging paging = request;

        string sql = $@"
SELECT
	FriendId AS UserId
	,u.Username AS UserName
	,MyFinLines.Amount
	,e.Name AS EntryName
	,e.CreatedOn AS EntryDate
	,UserIsDebtor
FROM (
SELECT Id AS LineId, Amount, DebtorId AS FriendId, EntryId, 1 AS UserIsDebtor
FROM EntryExpenses AS ee
WHERE ee.PayerId = @UserId

UNION ALL
SELECT Id AS LineId, Amount, PayerId AS FriendId, EntryId, 0 AS UserIsDebtor
FROM EntryExpenses AS ee
WHERE ee.DebtorId = @UserId) AS MyFinLines

INNER JOIN Entries AS e ON MyFinLines.EntryId = e.Id
INNER JOIN Users AS u ON MyFinLines.FriendId = u.Id

WHERE (@FriendId IS NULL OR MyFinLines.FriendId = @FriendId) AND e.GroupId = @GroupId
ORDER BY e.CreatedOn DESC
OFFSET {paging.Skip} ROWS FETCH NEXT {paging.Take} ROWS ONLY";

        Dictionary<string, object?> parameters = new()
        {
            { "UserId", request.UserId },
            { "FriendId", request.FriendId },
            { "GroupId", request.GroupId },
        };

        IEnumerable<EntryExpenseResponse> responses = await QueryList<EntryExpenseResponse>(sql, parameters);
        return responses.ToList();
    }
}