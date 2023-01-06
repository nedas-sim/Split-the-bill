using Application.Repositories;
using Domain.Common;
using Domain.Database;
using Infrastructure.Database;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories;

public sealed class EntryRepository : BaseRepository<Entry>, IEntryRepository
{
    public EntryRepository(DataContext context, IOptions<ConnectionStrings> options)
        : base(context, options.Value.DefaultConnection) 
    { 
    }
}