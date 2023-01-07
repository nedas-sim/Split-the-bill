namespace Domain.Responses.Finances;

public sealed class EntryResponse
{
    public Guid EntryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}