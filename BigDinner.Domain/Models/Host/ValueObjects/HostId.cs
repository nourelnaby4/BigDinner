namespace BigDinner.Domain.Models.Host.ValueObjects;

public sealed class HostId : ValueObject
{
    public Guid Value { get; }

    private  HostId(Guid value) 
    {
        Value = value;
    }

    public static HostId Create()
    {
        return new HostId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

