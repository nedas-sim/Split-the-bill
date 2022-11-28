using Application.Repositories;
using Domain.Common;
using Domain.Common.Identity;
using Domain.Common.Results;
using Domain.Exceptions;
using Domain.Responses;
using Domain.Results;
using MediatR;

namespace Application.Common;

public abstract class BaseCreateHandler<TRequest, TDatabaseEntity> : IRequestHandler<TRequest, BaseResult<CreateResponse>>
    where TRequest : BaseCreateRequest<TDatabaseEntity>
    where TDatabaseEntity : BaseEntity
{
    private readonly IBaseRepository<TDatabaseEntity> repository;

    public BaseCreateHandler(IBaseRepository<TDatabaseEntity> repository)
    {
        this.repository = repository;
    }

    public async Task<BaseResult<CreateResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await PreValidation(request);
            (request as IValidation).ValidateAndThrow();
            await DatabaseValidation(request, cancellationToken);
            TDatabaseEntity dbEntity = request.BuildEntity();
            await PostEntityBuilding(request, dbEntity, cancellationToken);

            await repository.Create(dbEntity, cancellationToken);
            return new CreateResponse { Id = dbEntity.Id };
        }
        catch (ValidationErrorException validationEx)
        {
            return new ValidationErrorResult<CreateResponse>
            {
                Message = validationEx.Message,
            };
        }
    }

    /// <summary>
    /// Called at the start
    /// </summary>
    public virtual async Task PreValidation(TRequest request)
    {
    }

    /// <summary>
    /// Called after request validation
    /// </summary>
    public virtual async Task DatabaseValidation(TRequest request, CancellationToken cancellationToken)
    {
    }

    /// <summary>
    /// Called after request is mapped to entity which is not yet added to the database
    /// </summary>
    public virtual async Task PostEntityBuilding(TRequest request, TDatabaseEntity entity, CancellationToken cancellationToken)
    {
    }
}