using Application.Common;
using Application.Repositories;
using Domain.Responses.Payments;

namespace Application.Payments.GetPaymentList;

public sealed class GetPaymentListQueryHandler : BaseListHandler<GetPaymentListQuery, PaymentResponse>
{
    private readonly IPaymentRepository paymentRepository;

    public GetPaymentListQueryHandler(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    public override async Task<List<PaymentResponse>> GetResponses(GetPaymentListQuery request, CancellationToken cancellationToken)
        => await paymentRepository.GetPaymentResponseList(request, cancellationToken);

    public override async Task<int> GetTotalCount(GetPaymentListQuery request, CancellationToken cancellationToken)
        => await paymentRepository.GetCount(cancellationToken);
}