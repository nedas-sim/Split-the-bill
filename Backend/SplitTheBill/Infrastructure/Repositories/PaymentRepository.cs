using Application.Repositories;
using Domain.Common;
using Domain.Database;
using Domain.Responses.Payments;
using Infrastructure.Database;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories;

public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(DataContext context, IOptions<ConnectionStrings> options) 
        : base(context, options.Value.DefaultConnection)
    {
    }

    public async Task<PaymentResponse?> GetPaymentResponse(Guid paymentId, CancellationToken cancellationToken = default)
    {
        PaymentResponse? paymentResponse =
            await context.Payments
                         .AsNoTracking()
                         .Where(p => p.Id == paymentId)
                         .Select(p => new PaymentResponse
                         {
                             Amount = p.Amount,
                             Currency = p.Currency.ToString(),
                             Date = p.DateOfPayment,
                             Id = p.Id,
                         })
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
                         .Select(p => new PaymentResponse
                         {
                             Amount = p.Amount,
                             Currency = p.Currency.ToString(),
                             Date = p.DateOfPayment,
                             Id = p.Id,
                         })
                         .ToListAsync(cancellationToken);

        return paymentResponses;
    }
}