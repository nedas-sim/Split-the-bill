using Domain.Common.Results;
using Domain.Database;
using MediatR;

namespace Application.Payments.DeletePayment;

public sealed record DeletePaymentCommand(PaymentId Id) : IRequest<BaseResult<Unit>>
{
    public DeletePaymentCommand(Guid id) : this(new PaymentId { Id = id }) { }
}