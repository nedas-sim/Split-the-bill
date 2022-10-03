using Application.Repositories;
using Domain.Common.Identity;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>, new()
    where TId : DatabaseEntityId
{
    protected readonly DataContext context;

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

    public async Task<int> GetCount(CancellationToken cancellationToken = default)
    {
        int count =
            await context.Set<TEntity>()
                         .CountAsync(cancellationToken);

        return count;
    }

    public async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        context.Set<TEntity>()
               .Update(entity);

        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<bool> Delete(TId id, CancellationToken cancellationToken = default)
    {
        TEntity entity = new() { Id = id };

        EntityEntry<TEntity> entry = context.Entry(entity);
        entry.State = EntityState.Deleted;

        try
        {
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
}