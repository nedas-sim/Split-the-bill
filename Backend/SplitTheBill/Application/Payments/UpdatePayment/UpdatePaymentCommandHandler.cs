using Application.Repositories;
using Domain.Common.Results;
using Domain.Database;
using Domain.Results;
using MediatR;

namespace Application.Payments.UpdatePayment;

public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, BaseResult<Unit>>
{
    private readonly IPaymentRepository paymentRepository;

    public UpdatePaymentCommandHandler(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    public async Task<BaseResult<Unit>> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        if (request.IsValid(out string? errorMessage) is false)
        {
            return new ValidationErrorResult<Unit>
            {
                Message = errorMessage!,
            };
        }

        Payment? payment = await paymentRepository.GetById(request.Id, cancellationToken);
        if (payment is null)
        {
            return new NotFoundResult<Unit>
            {
                Message = "Payment not found",
            };
        }

        request.Update(payment);
        await paymentRepository.Update(payment, cancellationToken);
        return new NoContentResult<Unit>();
    }
}