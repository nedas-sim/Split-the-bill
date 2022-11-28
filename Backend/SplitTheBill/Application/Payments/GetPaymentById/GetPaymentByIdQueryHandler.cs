using Application.Common;
using Application.Repositories;
using Domain.Common.Results;
using Domain.Extensions;
using Domain.Responses.Payments;

namespace Application.Payments.GetPaymentById;

public class GetPaymentByIdQueryHandler : IResultHandler<GetPaymentByIdQuery, PaymentResponse>
{
    private readonly IPaymentRepository paymentRepository;

    public GetPaymentByIdQueryHandler(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    public async Task<BaseResult<PaymentResponse>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        PaymentResponse? paymentResponse = await paymentRepository.GetPaymentResponse(request.Id, cancellationToken);

        if (paymentResponse is null)
        {
            return "Payment not found".ToNotFoundResult<PaymentResponse>();
        }

        return paymentResponse;
    }
}