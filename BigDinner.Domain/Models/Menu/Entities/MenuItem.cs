using BigDinner.Domain.Models.Menu.ValueObjects;

namespace BigDinner.Domain.Models.Menu.Entities;

public sealed class MenuItem : Entity<MenueItemId>
{
    public string Name { get; }
    public string Description { get; }

    private MenuItem(MenueItemId id,string name,string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public static MenuItem Create(string name,string description)
    {
        return new MenuItem(MenueItemId.Create(), name,description);
    }
}

