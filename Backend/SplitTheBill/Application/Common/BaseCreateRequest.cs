using Domain.Common;
using Domain.Common.Identity;
using Domain.Common.Results;
using Domain.Responses;
using MediatR;

namespace Application.Common;

public abstract class BaseCreateRequest<TDatabaseEntity> : BaseValidation, IRequest<BaseResult<CreateResponse>>
    where TDatabaseEntity : BaseEntity
{
    public abstract TDatabaseEntity BuildEntity();
}