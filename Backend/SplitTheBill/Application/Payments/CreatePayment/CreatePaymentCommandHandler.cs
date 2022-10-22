using Application.Common;
using Application.Repositories;
using Domain.Database;

namespace Application.Payments.CreatePayment;

public class CreatePaymentCommandHandler : BaseCreateHandler<CreatePaymentCommand, Payment>
{
    private readonly IPaymentRepository paymentRepository;

    public CreatePaymentCommandHandler(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    public override async Task InsertionToDatabase(CreatePaymentCommand request, Payment entity, CancellationToken cancellationToken)
    {
        await paymentRepository.Create(entity, cancellationToken);
    }
}