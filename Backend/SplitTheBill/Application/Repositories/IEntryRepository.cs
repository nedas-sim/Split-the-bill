using Application.Finances.GetEntryExpenses;
using Application.Finances.GetGroupEntries;
using Domain.Database;
using Domain.Responses.Finances;

namespace Application.Repositories;

public interface IEntryRepository : IBaseRepository<Entry>
{
    public Task<int> GetGroupEntryCount(GetGroupEntriesQuery request, CancellationToken cancellationToken);
    public Task<List<EntryResponse>> GetGroupEntries
        (GetGroupEntriesQuery request, 
         CancellationToken cancellationToken);

    public Task<int> GetEntryExpenseCount(GetEntryExpensesQuery request, CancellationToken cancellationToken);
    public Task<List<EntryExpenseResponse>> GetEntryExpenses
        (GetEntryExpensesQuery request,
         CancellationToken cancellationToken);
}