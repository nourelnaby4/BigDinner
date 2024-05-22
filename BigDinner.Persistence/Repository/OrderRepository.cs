using BigDinner.Application.Common.Abstractions.Cache;
using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Orders;

namespace BigDinner.Persistence.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IRedisCacheService _cache;
    private const string key = "order";

    public OrderRepository(ApplicationDbContext context, IRedisCacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public void Add(Order order)
    {
        _context.Add(order);

        _cache.Invalidate(key).Wait();
    }


    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _cache.Get(key, async () =>
        {
            return await _context.Orders
                .Include(x => x.Items)
                .Include(x => x.Shipping)
                .ToListAsync();
        });
    }
    public async Task<Order?> GetById(Guid orderId)
    {
        return await _cache.Get($"{key}-{orderId}", async () =>
        {
           return await _context.Orders
           .Include(x => x.Items)
           .FirstOrDefaultAsync(x => x.Id == orderId);
        });
    }
}
