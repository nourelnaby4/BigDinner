using BigDinner.Domain.Models.Menus.Aggregates;
using BigDinner.Domain.ValueObjects;

namespace BigDinner.Domain.Models.Menus.Entities;

public sealed class MenuItem : Entity<Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Price Price  { get; private set; }
    public Menu Menu { get; private set; }

    private MenuItem(Guid id,string name,string description,Price price) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    public static MenuItem Create(string name,string description,Price price)
    {
        return new MenuItem(Guid.NewGuid(), name,description,price);
    }
}

