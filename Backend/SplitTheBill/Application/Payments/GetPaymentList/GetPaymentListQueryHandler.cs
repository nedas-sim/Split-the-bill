﻿using Application.Repositories;
using Domain.Responses.Payments;
using Domain.Results;
using MediatR;

namespace Application.Payments.GetPaymentList;

public class GetPaymentListQueryHandler : IRequestHandler<GetPaymentListQuery, ListResult<PaymentResponse>>
{
    private readonly IPaymentRepository paymentRepository;

    public GetPaymentListQueryHandler(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    public async Task<ListResult<PaymentResponse>> Handle(GetPaymentListQuery request, CancellationToken cancellationToken)
    {
        List<PaymentResponse> payments = await paymentRepository.GetPaymentResponseList(request, cancellationToken);
        int totalCount = await paymentRepository.GetCount(cancellationToken);

        return new ListResult<PaymentResponse>(payments, totalCount, request);
    }
}