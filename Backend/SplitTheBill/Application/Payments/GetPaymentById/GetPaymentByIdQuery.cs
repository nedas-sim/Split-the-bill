using Domain.Common.Results;
using Domain.Responses.Payments;
using MediatR;

namespace Application.Payments.GetPaymentById;

public sealed record GetPaymentByIdQuery(Guid Id) : IRequest<BaseResult<PaymentResponse>>
{
}