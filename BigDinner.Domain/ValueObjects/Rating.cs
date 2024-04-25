namespace BigDinner.Domain.ValueObjects;

public sealed class Rating : ValueObject
{
    public int Value { get; private set; } // The value of the rating

    private Rating(int value)
    {
        Value = value;
    }

    public static Rating Create(int value)
    {
        if (value < 0 || value > 5) // Assuming ratings are between 0 and 5
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Rating value must be between 0 and 5.");
        }

        return new Rating(value);
    }

    // Override equality components
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}


