namespace BigDinner.Domain.ValueObjects;

public class Price
{
    public decimal Value { get; }
    public string Currency { get; }

    public Price(decimal value, string currency)
    {
        Value = value;
        Currency = currency;
    }
}


