using Application.Repositories;
using Domain.Common.Identity;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : DatabaseEntityId
{
    private readonly DataContext context;

    public BaseRepository(DataContext context)
    {
        this.context = context;
    }

    public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default)
    {
        context.Set<TEntity>()
               .Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<TEntity?> GetById(TId id, CancellationToken cancellationToken = default)
    {
        TEntity? entity =
            await context.Set<TEntity>()
                         .FirstOrDefaultAsync(x => x.Id == id,
                                              cancellationToken);

        return entity;
    }
}