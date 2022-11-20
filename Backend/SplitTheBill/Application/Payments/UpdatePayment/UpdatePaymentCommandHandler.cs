using Application.Common;
using Application.Repositories;
using Domain.Database;
using Domain.Responses.Payments;

namespace Application.Payments.UpdatePayment;

public class UpdatePaymentCommandHandler : BaseUpdateHandler<UpdatePaymentCommand, Payment, PaymentResponse>
{
    public UpdatePaymentCommandHandler(IPaymentRepository paymentRepository) : base(paymentRepository)
    {
    }

    public override async Task<PaymentResponse> BuildResponse(UpdatePaymentCommand request, Payment databaseEntity)
    {
        return new(databaseEntity);
    }
}