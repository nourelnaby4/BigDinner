using BigDinner.Domain.Models.Menus;

namespace BigDinner.Persistence.Repository;

public class MenuRepository : IMenuRepository
{
    private readonly ApplicationDbContext _context;

    public MenuRepository(ApplicationDbContext context)
        => _context = context;

    public void Add(Menu menu)
    {
        _context.Add(menu);
    }

    public async Task<IEnumerable<Menu>> GetAll()
    {
        return await _context.Menus
            .Include(x => x.MenuItem)
            .ToListAsync();
    }

    public async Task<Menu?> GetById(Guid MenuId)
    {
        return await _context.Menus
            .Where(x => x.Id == MenuId)
           .Include(x => x.MenuItem)
           .SingleOrDefaultAsync();
    }
}
