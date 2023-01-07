using Application.Common;
using Domain.Common;
using Domain.Database;

namespace Application.Finances.CreateEntry;

public sealed class CreateEntryCommand : BaseCreateRequest<Entry>
{
    internal Guid ActualPayerId => PayerId ?? UserId;

    #region API Params
    public string Name { get; set; }
	public string? Description { get; set; }
	public List<FinancialLine> FinancialLines { get; set; }
	public Guid? PayerId { get; set; }
    public Guid GroupId { get; set; }
    #endregion
    #region Auth ID
    internal Guid UserId { get; set; }
    public void SetUserId(Guid id) => UserId = id;
    #endregion
    #region Overrides
    public override Entry BuildEntity()
    {
        Entry entry = new()
        {
            Name = Name,
            GroupId = GroupId,
            Description = Description,
            Expenses = FinancialLines.Select(fl => new EntryExpense
            {
                Amount = fl.Amount,
                DebtorId = fl.DebtorId,
                PayerId = ActualPayerId,
            }).ToList(),
        };

        return entry;
    }

    public override string ApiErrorMessagePrefix() => ErrorMessages.Finances.CreateRequestPrefix;

    public override IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        bool anyFinanceLines = FinancialLines?.Any() ?? false;
        bool nonNegativeAmounts = FinancialLines?.All(fl => fl.Amount >= 0) ?? true;
        bool payerIsDebtor = FinancialLines?.Any(fl => fl.DebtorId == ActualPayerId) ?? true;

        yield return (anyFinanceLines, ErrorMessages.Finances.NoExpenseLines);
        yield return (nonNegativeAmounts, ErrorMessages.Finances.LineWithNegativeAmount);
        yield return (payerIsDebtor is false, ErrorMessages.Finances.PayerIsDebtor);
    }
    #endregion
}

public sealed class FinancialLine
{
	public Guid DebtorId { get; set; }
	public decimal Amount { get; set; }
}