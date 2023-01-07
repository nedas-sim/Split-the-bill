using Application.Repositories;
using Dapper;
using Domain.Common.Identity;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.SqlClient;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity, new()
{
    protected readonly DataContext context;
    readonly string ConnectionString;

    public BaseRepository(DataContext context, string connectionString)
    {
        this.context = context;
        ConnectionString = connectionString;
    }

    public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default)
    {
        context.Set<TEntity>()
               .Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken = default)
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

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken = default)
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

    protected async Task<IEnumerable<TResponse>> QueryList<TResponse>(string query, Dictionary<string, object?> parameters)
    {
        using SqlConnection connection = new(ConnectionString);
        connection.Open();
        IEnumerable<TResponse> responses = await
            connection.QueryAsync<TResponse>(query, parameters);

        return responses;
    }

    protected async Task<T> QueryValue<T>(string query, Dictionary<string, object?> parameters)
    {
        using SqlConnection connection = new(ConnectionString);
        connection.Open();
        T value = (await connection.QueryAsync<T>(query, parameters)).First();
        return value;
    }
}