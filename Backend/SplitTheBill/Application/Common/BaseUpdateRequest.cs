using Domain.Common;
using Domain.Common.Identity;
using Domain.Common.Results;
using MediatR;

namespace Application.Common;

public abstract class BaseUpdateRequest<TDatabaseEntity, TResponse> : IValidation, IRequest<BaseResult<TResponse>>
    where TDatabaseEntity : BaseEntity
{
    internal Guid Id { get; set; }
    public void SetId(Guid id) => Id = id;

    public abstract void Update(TDatabaseEntity databaseEntity);

    public virtual string ApiErrorMessagePrefix() => string.Empty;
    public virtual IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        yield break;
    }
}