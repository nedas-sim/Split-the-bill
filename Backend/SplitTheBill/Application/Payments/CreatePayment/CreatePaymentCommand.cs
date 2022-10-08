using Application.Common;
using Domain.Database;
using Domain.Enums;
using Domain.Extensions;

namespace Application.Payments.CreatePayment;

public sealed class CreatePaymentCommand : BaseCreateRequest<Payment>
{
    public DateTime DateOfPayment { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }

    public override bool IsValid(out string? errorMessage)
    {
        bool dateInPast = DateOfPayment < DateTime.Now;
        bool positiveAmount = Amount > 0;
        bool validCurrency = Enum.IsDefined(Currency);

        List<string> errorMessages = new();
        errorMessages.AddIfFalse(dateInPast is true, "Date of payment has to be in the past")
                     .AddIfFalse(positiveAmount is true, "Amount has to be positive")
                     .AddIfFalse(validCurrency is true, "Currency is not supported");

        errorMessage = errorMessages.BuildErrorMessage("Create payment request has validation errors");
        return string.IsNullOrEmpty(errorMessage);
    }

    public override Payment BuildEntity()
    {
        Payment payment = new()
        {
            Amount = Amount,
            Currency = Currency,
            DateOfPayment = DateOfPayment,
        };

        return payment;
    }
}