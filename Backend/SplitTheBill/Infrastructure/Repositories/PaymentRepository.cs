using Application.Repositories;
using Domain.Common;
using Domain.Database;
using Domain.Responses.Payments;
using Infrastructure.Database;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentRepository : BaseRepository<Payment, PaymentId>, IPaymentRepository
{
    public PaymentRepository(DataContext context) : base(context)
    {
    }

    public async Task<PaymentResponse?> GetPaymentResponse(PaymentId paymentId, CancellationToken cancellationToken = default)
    {
        PaymentResponse? paymentResponse =
            await context.Payments
                         .AsNoTracking()
                         .Where(p => p.Id == paymentId)
                         .Select(p => new PaymentResponse(p))
                         .FirstOrDefaultAsync(cancellationToken);

        return paymentResponse;
    }

    public async Task<List<PaymentResponse>> GetPaymentResponseList(PagingParameters parameters, CancellationToken cancellationToken = default)
    {
        List<PaymentResponse> paymentResponses =
            await context.Payments
                         .AsNoTracking()
                         .OrderBy(p => p.DateOfPayment.Date)
                         .ApplyPaging(parameters)
                         .Select(p => new PaymentResponse(p))
                         .ToListAsync(cancellationToken);

        return paymentResponses;
    }
}