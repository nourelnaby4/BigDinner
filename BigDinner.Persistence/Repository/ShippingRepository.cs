using BigDinner.Domain.Models.Shippings;

namespace BigDinner.Persistence.Repository;

public class ShippingRepository : IShippingRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IRedisCacheService _cache;

    public ShippingRepository(ApplicationDbContext context, IRedisCacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public void Add(Shipping shipping)
    {
        _context.Add(shipping);
        _cache.Invalidate("shippings");
    }


    public async Task<List<Shipping>> GetAll()
    {
        string key = "shippings";

        return await _cache.Get(key, async () =>
        {
            return await _context.Shipping
            .Include(x => x.ShippingMethod)
            .ToListAsync();
        });
    }


    public async Task<Shipping> GetByIdAsync(Guid id)
    {
        string key = $"shipping-{id}";

        return await _cache.Get(key, async () =>
        {
            return await _context.Shipping
        .Include(x => x.Order)
        .Include(x => x.Address)
        .SingleOrDefaultAsync(x => x.Id == id);
        });
    }

    public void Update(Shipping shipping)
    {
        _context.Update(shipping);
        _cache.Invalidate("shippings");
    }
}