namespace BigDinner.Domain.Models.Menu.Entities;

public class MenuSection : Entity<MenuSectionId>
{
    private readonly List<MenuItem> _items=new();
    public string Name { get; }
    public string Description { get; }
    
    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

    private MenuSection(MenuSectionId id,string name,string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public static MenuSection Create(string name, string description)
    {
        return new MenuSection(MenuSectionId.Create(),name,description);
    }

}

