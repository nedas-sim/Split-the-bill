using Application.Repositories;
using Domain.Common.Identity;
using Domain.Common.Results;
using Domain.Exceptions;
using Domain.Results;
using MediatR;

namespace Application.Common;

public abstract class BaseUpdateHandler<TRequest, TDatabaseEntity> : IRequestHandler<TRequest, BaseResult<Unit>>
    where TRequest : BaseUpdateRequest<TDatabaseEntity>
    where TDatabaseEntity : BaseEntity
{
    private readonly IBaseRepository<TDatabaseEntity> repository;

    public BaseUpdateHandler(IBaseRepository<TDatabaseEntity> repository)
    {
        this.repository = repository;
    }

    public async Task<BaseResult<Unit>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await PreValidation(request);
            request.ValidateAndThrow();
            TDatabaseEntity databaseEntity = await repository.GetById(request.Id, cancellationToken)
                ?? throw new NotFoundErrorException($"{typeof(TDatabaseEntity).Name} not found");

            await DatabaseValidation(request, databaseEntity, cancellationToken);

            request.Update(databaseEntity);
            await repository.Update(databaseEntity, cancellationToken);
            return new NoContentResult<Unit>();
        }
        catch (ValidationErrorException validationEx)
        {
            return new ValidationErrorResult<Unit>
            {
                Message = validationEx.Message,
            };
        }
        catch (NotFoundErrorException notFoundEx)
        {
            return new NotFoundResult<Unit>
            {
                Message = notFoundEx.Message,
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
    /// Called after request validation and entity retrieval by id
    /// </summary>
    public virtual async Task DatabaseValidation(TRequest request, TDatabaseEntity databaseEntity, CancellationToken cancellationToken)
    {
    }
}