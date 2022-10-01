using Application.Repositories;
using Domain.Common.Results;
using Domain.Database;
using Domain.Results;
using MediatR;

namespace Application.Payments.CreatePayment;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, BaseResult<Unit>>
{
    private readonly IPaymentRepository paymentRepository;

    public CreatePaymentCommandHandler(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    public async Task<BaseResult<Unit>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        string errorMessage = request.ValidateAndGetErrorMessage();

        if (string.IsNullOrEmpty(errorMessage) is false)
        {
            return new ValidationErrorResult<Unit>
            {
                Message = errorMessage,
            };
        }

        Payment payment = request.BuildEntity();
        await paymentRepository.Create(payment, cancellationToken);
        return new NoContentResult<Unit>();
    }
}