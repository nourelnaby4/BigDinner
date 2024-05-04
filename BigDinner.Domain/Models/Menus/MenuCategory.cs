namespace BigDinner.Domain.Models.Menus;

public sealed class MenuCategory : AggregateRoot<Guid>
{
    private MenuCategory(Guid id) : base(id)
    {
    }

    private readonly List<Menu> _menus = new();

    public string Name { get; private set; }

    public string Description { get; private set; }

    public IReadOnlyList<Menu> Menus => _menus.ToList();

    private MenuCategory(Guid id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public static MenuCategory Create(Guid id, string name, string description)
    {
        return new MenuCategory(id, name, description);
    }

    public void AddMenu(Menu item)
    {
        _menus.Add(item);
    }

    public void RemoveMenu(Menu item)
    {
        _menus.Remove(item);
    }
}

