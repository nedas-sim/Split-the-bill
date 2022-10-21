using Domain.Common.Results;
using MediatR;

namespace Application.Common;

public interface IResultRequest<TResponse> : IRequest<BaseResult<TResponse>>
{
}