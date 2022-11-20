using Application.Common;
using Domain.Database;
using Domain.Enums;
using Domain.Extensions;
using Domain.Responses.Payments;

namespace Application.Payments.UpdatePayment;

public sealed class UpdatePaymentCommand : BaseUpdateRequest<Payment, PaymentResponse>
{
    public DateTime? DateOfPayment { get; set; }
    public decimal? Amount { get; set; }
    public Currency? Currency { get; set; }

    public override void Update(Payment databaseEntity)
    {
        databaseEntity.DateOfPayment = DateOfPayment ?? databaseEntity.DateOfPayment;
        databaseEntity.Amount = Amount ?? databaseEntity.Amount;
        databaseEntity.Currency = Currency ?? databaseEntity.Currency;
    }

    public override bool IsValid(out string? errorMessage)
    {
        List<string> errorMessages = new();

        bool isDateValid = DateOfPayment.HasValue is false || (DateOfPayment.HasValue && DateOfPayment < DateTime.Now);
        bool isAmountValid = Amount.HasValue is false || (Amount.HasValue && Amount > 0);
        bool isCurrencyValid = Currency.HasValue is false || (Currency.HasValue && Enum.IsDefined(Currency.Value));

        errorMessages.AddIfFalse(isDateValid, "Date of payment has to be in the past")
                     .AddIfFalse(isAmountValid, "Amount has to be positive")
                     .AddIfFalse(isCurrencyValid, "Currency is not supported");

        errorMessage = errorMessages.BuildErrorMessage("Update payment request has validation errors");
        return string.IsNullOrEmpty(errorMessage);
    }
}