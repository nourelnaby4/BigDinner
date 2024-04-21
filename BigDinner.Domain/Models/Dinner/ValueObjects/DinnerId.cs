namespace BigDinner.Domain.Models.Dinner.ValueObjects;

public sealed class DinnerId : ValueObject
{
    public Guid Value { get; }

    private DinnerId(Guid value) 
    {
        Value = value;
    }

    public static DinnerId Create()
    {
        return new DinnerId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

