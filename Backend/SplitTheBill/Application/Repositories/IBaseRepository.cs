using Domain.Common.Identity;

namespace Application.Repositories;

public interface IBaseRepository<TEntity, TId> 
    where TEntity : BaseEntity<TId>
    where TId : DatabaseEntityId

{
    public Task<TEntity?> GetById(TId id, CancellationToken cancellationToken = default);
    public Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default);
    public Task<int> GetCount(CancellationToken cancellationToken = default);
    public Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
}