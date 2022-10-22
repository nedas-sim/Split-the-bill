using Application.Common;
using Application.Repositories;
using Domain.Database;

namespace Application.Payments.CreatePayment;

public class CreatePaymentCommandHandler : BaseCreateHandler<CreatePaymentCommand, Payment>
{
    public CreatePaymentCommandHandler(IPaymentRepository paymentRepository) : base(paymentRepository)
    {
    }
}