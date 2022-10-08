using Domain.Common;
using Domain.Database;
using Domain.Responses.Payments;

namespace Application.Repositories;

public interface IPaymentRepository : IBaseRepository<Payment, PaymentId>
{
    Task<PaymentResponse?> GetPaymentResponse(PaymentId paymentId, CancellationToken cancellationToken = default);
    Task<List<PaymentResponse>> GetPaymentResponseList(PagingParameters parameters, CancellationToken cancellationToken = default);
}