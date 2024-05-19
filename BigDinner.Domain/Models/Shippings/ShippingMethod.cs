using Newtonsoft.Json;

namespace BigDinner.Domain.Models.Shippings;

public class ShippingMethod : Entity<Guid>
{
    public string Name { get; private set; }

    public string Description { get; private set; }

    [JsonConstructor]
    private ShippingMethod(Guid id) : base(id)
    {
    }

    private ShippingMethod(Guid id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public static ShippingMethod Create(string name, string description)
    {
        return new ShippingMethod(Guid.NewGuid(), name, description);
    }
}
