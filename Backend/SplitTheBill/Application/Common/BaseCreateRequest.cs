using Domain.Common.Identity;
using Domain.Common.Results;
using MediatR;

namespace Application.Common;

public abstract class BaseCreateRequest<TDatabaseEntity, TId> : IRequest<BaseResult<Unit>>
    where TDatabaseEntity : BaseEntity<TId>
    where TId : DatabaseEntityId
{
    public virtual bool IsValid(out string? errorMessage)
    {
        // By default the request is valid
        errorMessage = null;
        return true;
    }

    public abstract TDatabaseEntity BuildEntity();
}