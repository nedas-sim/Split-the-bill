using Domain.Common.Results;
using Domain.Database;
using Domain.Responses.Payments;
using MediatR;

namespace Application.Payments.GetPaymentById;

public sealed record GetPaymentByIdQuery(PaymentId Id) : IRequest<BaseResult<PaymentResponse>>
{
	public GetPaymentByIdQuery(Guid id) : this(new PaymentId { Id = id }) { }
}