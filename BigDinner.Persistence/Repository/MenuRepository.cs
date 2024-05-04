using BigDinner.Domain.Models.Menus;

namespace BigDinner.Persistence.Repository;

public class MenuRepository : IMenuRepository
{
    private readonly ApplicationDbContext _context;

    public MenuRepository(ApplicationDbContext context) 
        => _context = context;

    public void Add(MenuCategory menu)
    {
        _context.Add(menu);
    }

    public IEnumerable<MenuCategory> GetAll()
    {
        throw new NotImplementedException();
    }

    public MenuCategory GetById()
    {
        throw new NotImplementedException();
    }
}
