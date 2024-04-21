namespace BigDinner.Domain.Models.Menu.ValueObjects;

public sealed class MenueItemId : ValueObject
{
    public Guid Value { get; }

    private MenueItemId(Guid value)
    {
        Value = value;
    }

    public static MenueItemId Create()
    {
        return new MenueItemId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
