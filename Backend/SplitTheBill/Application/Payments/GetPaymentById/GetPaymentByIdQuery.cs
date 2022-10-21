using Application.Common;
using Domain.Responses.Payments;

namespace Application.Payments.GetPaymentById;

public sealed record GetPaymentByIdQuery(Guid Id) : IResultRequest<PaymentResponse>
{
}