using Application.Common;
using Application.Repositories;
using Domain.Common.Results;
using Domain.Responses.Payments;
using Domain.Results;

namespace Application.Payments.GetPaymentList;

public sealed class GetPaymentListQueryHandler : IListHandler<GetPaymentListQuery, PaymentResponse>
{
    private readonly IPaymentRepository paymentRepository;

    public GetPaymentListQueryHandler(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    public async Task<BaseListResult<PaymentResponse>> Handle(GetPaymentListQuery request, CancellationToken cancellationToken)
    {
        List<PaymentResponse> payments = await paymentRepository.GetPaymentResponseList(request, cancellationToken);
        int totalCount = await paymentRepository.GetCount(cancellationToken);

        return new ListResult<PaymentResponse>(payments, totalCount, request);
    }
}