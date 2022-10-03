using Domain.Common;

namespace Domain.ValueObjects;

public sealed class PastDateTime : ValueObject
{
    public DateTime Date { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Date;
    }

    public void UpdateDate(DateTime? date)
    {
        if (date is not null)
        {
            Date = date.Value;
        }
    }
}