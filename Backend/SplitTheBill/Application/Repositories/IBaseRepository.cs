﻿using Domain.Common.Identity;

namespace Application.Repositories;

public interface IBaseRepository<TEntity, TId> 
    where TEntity : BaseEntity<TId>
    where TId : DatabaseEntityId

{
    public Task<TEntity?> GetById(TId id);
    public Task<TEntity> Create(TEntity entity);
}