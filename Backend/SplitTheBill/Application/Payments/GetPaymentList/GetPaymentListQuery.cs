using Application.Common;
using Domain.Responses.Payments;

namespace Application.Payments.GetPaymentList;

public sealed class GetPaymentListQuery : BaseListRequest<PaymentResponse>
{
}