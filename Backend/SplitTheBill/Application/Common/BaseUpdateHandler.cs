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

    protected TDatabaseEntity databaseEntity;

    public BaseUpdateHandler(IBaseRepository<TDatabaseEntity> repository)
    {
        this.repository = repository;
    }

    public async Task<BaseResult<Unit>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            request.ValidateAndThrow();
            databaseEntity = await repository.GetById(request.Id, cancellationToken)
                ?? throw new NotFoundErrorException($"{typeof(TDatabaseEntity).Name} not found");

            await DatabaseValidation(request, cancellationToken);

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
    /// Called after request validation and entity retrieval by id
    /// </summary>
    public virtual async Task DatabaseValidation(TRequest request, CancellationToken cancellationToken)
    {
    }
}