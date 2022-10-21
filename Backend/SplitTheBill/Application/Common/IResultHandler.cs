using Domain.Common.Results;
using MediatR;

namespace Application.Common;

public interface IResultHandler<TRequest, TResponse> : IRequestHandler<TRequest, BaseResult<TResponse>>
    where TRequest : IResultRequest<TResponse>
{
}