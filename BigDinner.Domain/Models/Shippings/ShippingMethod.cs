namespace BigDinner.Domain.Models.Shippings;

public class ShippingMethod : Entity<Guid>
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    private ShippingMethod(Guid id) : base(id)
    {
    }

    private ShippingMethod(Guid id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public static ShippingMethod Create(Guid id, string name, string description)
    {
        return new ShippingMethod(id, name, description);
    }
}
