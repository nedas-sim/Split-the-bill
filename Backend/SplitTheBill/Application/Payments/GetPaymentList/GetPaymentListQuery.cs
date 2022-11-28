using Application.Common;
using Domain.Common;
using Domain.Responses.Payments;

namespace Application.Payments.GetPaymentList;

public sealed class GetPaymentListQuery : PagingParameters, IListRequest<PaymentResponse>
{
    public string ApiErrorMessagePrefix => throw new NotImplementedException();

    public IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        throw new NotImplementedException();
    }
}