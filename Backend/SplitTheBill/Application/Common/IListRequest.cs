using Domain.Common;
using Domain.Common.Results;
using MediatR;

namespace Application.Common;

public interface IListRequest<TResponse> : IRequest<BaseListResult<TResponse>>, IPaging, IValidation
{
}