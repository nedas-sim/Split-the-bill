using Application.Common;
using Domain.Database;
using Domain.Enums;
using Domain.Responses.Payments;

namespace Application.Payments.UpdatePayment;

public sealed class UpdatePaymentCommand : BaseUpdateRequest<Payment, PaymentResponse>
{
    #region API Params
    public DateTime? DateOfPayment { get; set; }
    public decimal? Amount { get; set; }
    public Currency? Currency { get; set; }
    #endregion
    #region Overrides
    public override string ApiErrorMessagePrefix => "Update payment request has validation errors";

    public override void Update(Payment databaseEntity)
    {
        databaseEntity.DateOfPayment = DateOfPayment ?? databaseEntity.DateOfPayment;
        databaseEntity.Amount = Amount ?? databaseEntity.Amount;
        databaseEntity.Currency = Currency ?? databaseEntity.Currency;
    }

    public override IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        bool isDateValid = DateOfPayment.HasValue is false || (DateOfPayment.HasValue && DateOfPayment < DateTime.Now);
        bool isAmountValid = Amount.HasValue is false || (Amount.HasValue && Amount > 0);
        bool isCurrencyValid = Currency.HasValue is false || (Currency.HasValue && Enum.IsDefined(Currency.Value));

        yield return (isDateValid, "Date of payment has to be in the past");
        yield return (isAmountValid, "Amount has to be positive");
        yield return (isCurrencyValid, "Currency is not supported");
    }
    #endregion
}