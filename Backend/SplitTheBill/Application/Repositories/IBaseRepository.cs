using Domain.Common.Identity;

namespace Application.Repositories;

public interface IBaseRepository<TEntity> 
    where TEntity : BaseEntity
{
    public Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken = default);
    public Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default);
    public Task<int> GetCount(CancellationToken cancellationToken = default);
    public Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
    public Task<bool> Delete(Guid id, CancellationToken cancellationToken = default);
}