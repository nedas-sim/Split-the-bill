using Application.Common;
using Application.Repositories;
using Domain.Database;

namespace Application.Payments.UpdatePayment;

public class UpdatePaymentCommandHandler : BaseUpdateHandler<UpdatePaymentCommand, Payment>
{
    public UpdatePaymentCommandHandler(IPaymentRepository paymentRepository) : base(paymentRepository)
    {
    }
}