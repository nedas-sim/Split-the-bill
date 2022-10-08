using Domain.Common;
using Domain.Database;
using Domain.Responses.Payments;

namespace Application.Repositories;

public interface IPaymentRepository : IBaseRepository<Payment>
{
    Task<PaymentResponse?> GetPaymentResponse(Guid paymentId, CancellationToken cancellationToken = default);
    Task<List<PaymentResponse>> GetPaymentResponseList(PagingParameters parameters, CancellationToken cancellationToken = default);
}