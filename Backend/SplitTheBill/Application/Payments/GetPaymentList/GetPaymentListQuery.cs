﻿using Application.Common;
using Domain.Common;
using Domain.Responses.Payments;
using Domain.Results;
using MediatR;

namespace Application.Payments.GetPaymentList;

public sealed class GetPaymentListQuery : PagingParameters, IListRequest<PaymentResponse>
{
}