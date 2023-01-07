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
}