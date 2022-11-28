using Application.Repositories;
using Domain.Common.Results;
using Domain.Extensions;
using Domain.Results;
using MediatR;

namespace Application.Payments.DeletePayment;

public sealed class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, BaseResult<Unit>>
{
    private readonly IPaymentRepository paymentRepository;

    public DeletePaymentCommandHandler(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    public async Task<BaseResult<Unit>> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        bool deleted = await paymentRepository.Delete(request.Id, cancellationToken);

        return deleted ?
            new NoContentResult<Unit>() : 
            "Payment not found".ToNotFoundResult<Unit>();
    }
}