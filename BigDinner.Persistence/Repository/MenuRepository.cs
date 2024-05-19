using BigDinner.Domain.Models.Menus;

namespace BigDinner.Persistence.Repository;

public class MenuRepository : IMenuRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IRedisCacheService _cache;

    public MenuRepository(ApplicationDbContext context, IRedisCacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public void Add(Menu menu)
    {
        _context.Add(menu);
        _cache.Invalidate("menus");
    }

    public async Task<List<Menu>> GetAll()
    {
        string key = "menus";

        return await _cache.Get(key, async () =>
        {
            return await _context.Menus
            .Include(x => x.Items)
            .ToListAsync();
        });
    }

    public async Task<Menu?> GetById(Guid MenuId)
    {
        string key = $"menus-{MenuId}";

        return await _cache.Get(key, async () =>
        {
            return await _context.Menus
            .Where(x => x.Id == MenuId)
           .Include(x => x.Items)
           .SingleOrDefaultAsync();
        });
    }
}
