using Domain.Common;
using Domain.Common.Identity;
using Domain.Common.Results;
using MediatR;

namespace Application.Common;

public abstract class BaseUpdateRequest<TDatabaseEntity, TResponse> : BaseValidation, IRequest<BaseResult<TResponse>>
    where TDatabaseEntity : BaseEntity
{
    internal Guid Id { get; set; }
    public void SetId(Guid id) => Id = id;

    public abstract void Update(TDatabaseEntity databaseEntity);
}