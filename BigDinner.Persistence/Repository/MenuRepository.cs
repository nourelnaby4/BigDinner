using BigDinner.Domain.Models.Menus;

namespace BigDinner.Persistence.Repository;

public class MenuRepository : IMenuRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IRedisCacheService _cache;
   // public static string key = "menu";

    public MenuRepository(ApplicationDbContext context, IRedisCacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public  void Add(Menu menu)
    {
        _context.Add(menu);
       _cache.Invalidate($"{menu}").Wait();
    }

    public async Task<List<Menu>> GetAsync()
    {
        string key = "menu";
        return await _cache.Get(key, async () =>
        {
            return await _context.Menus
            .Include(x => x.Items)
            .OrderByDescending(x => x.LastUpdateDateOnUtc)
            .ThenBy(x => x.CreateDateOnUtc)
            .ToListAsync();

        });


    }

    public async Task<Menu?> GetByIdAsync(Guid MenuId)
    {
        //return await _cache.Get($"{key}-{MenuId}", async () =>
        //{
            
        //});
        return await _context.Menus
              .Where(x => x.Id == MenuId)
              .Include(x => x.Items)
              .SingleOrDefaultAsync();
    }

    public  void Update(Menu menu)
    {
        _context.Update(menu);
        _cache.Invalidate($"{menu}").Wait();
        _cache.Invalidate($"{menu}-{menu.Id}").Wait();
    }

    public async Task<Menu?> GetMenuItemByIdAsync(Guid menuId, Guid menuItemId)
    {
        return await _context.Menus
             .Where(x => x.Id == menuId)
             .Include(x => x.Items.Where(it=>it.Id==menuItemId))
             .AsSplitQuery()
             .SingleOrDefaultAsync();
    }
}
