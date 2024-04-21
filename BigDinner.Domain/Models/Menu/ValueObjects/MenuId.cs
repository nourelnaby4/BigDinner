namespace BigDinner.Domain.Models.Menu.ValueObjects;

public sealed class MenuId : ValueObject
{
    public Guid Value { get; }

    private MenuId(Guid value) 
    {
        Value = value;
    }

    public static MenuId Create()
    {
        return new MenuId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

