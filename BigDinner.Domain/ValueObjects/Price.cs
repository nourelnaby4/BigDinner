using System.ComponentModel.DataAnnotations.Schema;

namespace BigDinner.Domain.ValueObjects;

[ComplexType]
public class Price : ValueObject
{
    public decimal Value { get; private set; }
    public string Currency { get; private set; }

    public Price(decimal value, string? currency= "EGP")
    {
        Value = value;
        Currency = currency;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Currency;
    }
}


