namespace Domain.Responses.Finances;

public sealed class EntryExpenseResponse
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public decimal Amount { get; set; }
    public string EntryName { get; set; }
    public DateTime EntryDate { get; set; }
    public bool UserIsDebtor { get; set; }
}