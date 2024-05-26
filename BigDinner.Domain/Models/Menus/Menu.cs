using Newtonsoft.Json;

namespace BigDinner.Domain.Models.Menus;

public sealed class Menu : AggregateRoot<Guid>
{
    private  List<MenuItem> _items = new();

    public string Name { get; private set; }

    public string Description { get; private set; }

    public float AverageRating { get; private set; }

    public DateTime CreateDateOnUtc { get; private set; }

    public DateTime LastUpdateDateOnUtc { get; private set; }

    [JsonIgnore]
    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

    [JsonConstructor]
    private Menu(Guid id) : base(id) { }
     
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
    public void UpdateMenu(string name,string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty.");
        }

        Name = name;
        Description = description;
        LastUpdateDateOnUtc = DateTime.UtcNow;
    }


    public void AddMenuItem(string name, string description, Price price)
    {
        var menuItem = MenuItem.Create(this.Id, name, description, price);
        _items.Add(menuItem);
         LastUpdateDateOnUtc = DateTime.UtcNow;
    }

    public void RemoveMenuItem(MenuItem item)
    {
        _items.Remove(item);
        LastUpdateDateOnUtc = DateTime.UtcNow;
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

