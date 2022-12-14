using Domain.Common;
using Domain.Common.Results;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Results;
using MediatR;

namespace Application.Common;

public abstract class BaseListHandler<TRequest, TResponse> : IRequestHandler<TRequest, BaseListResult<TResponse>>
    where TRequest : BaseListRequest<TResponse>
{
    public async Task<BaseListResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await PreValidation(request);
            (request as IValidation).ValidateAndThrow();
            await DatabaseValidation(request, cancellationToken);
            List<TResponse> responses = await GetResponses(request, cancellationToken);
            int totalCount = await GetTotalCount(request, cancellationToken);

            return new ListResult<TResponse>(responses, totalCount, request);
        }
        catch (ValidationErrorException validationEx)
        {
            return validationEx.Message.ToListValidationResult<TResponse>();
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

    public abstract Task<List<TResponse>> GetResponses(TRequest request, CancellationToken cancellationToken);
    public abstract Task<int> GetTotalCount(TRequest request, CancellationToken cancellationToken);
}