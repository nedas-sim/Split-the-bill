using Domain.Common.Results;
using MediatR;

namespace Application.Payments.DeletePayment;

public sealed record DeletePaymentCommand(Guid Id) : IRequest<BaseResult<Unit>>
{
}