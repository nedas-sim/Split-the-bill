using Domain.Common;
using Domain.Enums;

namespace Domain.ValueObjects;

public class Amount : ValueObject
{
    public decimal Value { get; set; }
    public Currency Currency { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Currency;
    }

    public void UpdateValue(decimal? value)
    {
        if (value is not null)
        {
            Value = value.Value;
        }
    }

    public void UpdateCurrency(Currency? currency)
    {
        if (currency is not null)
        {
            Currency = currency.Value;
        }
    }
}