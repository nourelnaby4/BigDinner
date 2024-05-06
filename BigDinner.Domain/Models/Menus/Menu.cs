namespace BigDinner.Domain.Models.Menus;

public sealed class Menu : AggregateRoot<Guid>
{
    private readonly List<MenuItem> _items = new();

    public string Name { get; private set; }

    public string Description { get; private set; }

    public float AverageRating { get; private set; }

    public DateTime CreateDateOnUtc { get; private set; }

    public DateTime LastUpdateDateOnUtc { get; private set; }

    public IReadOnlyList<MenuItem> Items => _items.ToList();

    public MenuItem MenuItem { get; private set; }

     
    private Menu(Guid id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
        CreateDateOnUtc = DateTime.UtcNow;
        LastUpdateDateOnUtc = DateTime.UtcNow;
    }

    public static Menu Create(string name, string description)
    {
        var menu= new Menu(Guid.NewGuid(), name, description);

        menu.RaiseDomainEvent(new MenuCreateDomainEvent(new MenuCreateEventMessage(menu.Id,menu.Name)));

        return menu;
    }

    public void AddMenuItem(MenuItem item)
    {
        _items.Add(item);
    }

    public void RemoveMenuItem(MenuItem item)
    {
        _items.Remove(item);
    }

    public Price CalculateTotalPrice()
    {
        decimal totalPrice = 0;

        foreach (var menuItem in _items)
        {
            totalPrice += menuItem.Price.Value;
        }

        return new Price(totalPrice);
    }
}

