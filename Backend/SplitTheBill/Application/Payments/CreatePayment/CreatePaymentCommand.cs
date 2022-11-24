using Application.Common;
using Domain.Database;
using Domain.Enums;

namespace Application.Payments.CreatePayment;

public sealed class CreatePaymentCommand : BaseCreateRequest<Payment>
{
    public DateTime DateOfPayment { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }

    public override string ApiErrorMessagePrefix => "Create payment request has validation errors";

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

    public override IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        bool dateInPast = DateOfPayment < DateTime.Now;
        bool positiveAmount = Amount > 0;
        bool validCurrency = Enum.IsDefined(Currency);

        yield return (dateInPast, "Date of payment has to be in the past");
        yield return (positiveAmount, "Amount has to be positive");
        yield return (validCurrency, "Currency is not supported");
    }
}