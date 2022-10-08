using Domain.Common.Identity;
using Domain.Common.Results;
using MediatR;

namespace Application.Common;

public abstract class BaseUpdateRequest<TDatabaseEntity, TId> : IRequest<BaseResult<Unit>>
    where TDatabaseEntity : BaseEntity<TId>
    where TId : DatabaseEntityId, new()
{
    // Internal to set the value in controller and
    // not to map it from request body
    internal TId Id { get; private set; }

    public abstract void Update(TDatabaseEntity databaseEntity);
    public virtual bool IsValid(out string? errorMessage)
    {
        // By default the request is valid
        errorMessage = null;
        return true;
    }

    public void SetId(TId id)
    {
        Id = id;
    }
}