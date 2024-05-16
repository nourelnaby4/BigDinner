using BigDinner.Application.Common.Abstractions.Cache;
using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Orders;

namespace BigDinner.Persistence.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMemoryCacheService _memoryCacheService;

    public OrderRepository(ApplicationDbContext context, IMemoryCacheService cacheService)
    {
        _context = context;
        _memoryCacheService = cacheService;
    }

    public void Add(Order order)
        => _context.Add(order);


    public async Task<IEnumerable<Order>> GetAll()
    {
        var key = "orderList";

        return await _memoryCacheService.Get(key, async () =>
        {
            return await _context.Orders
                .Include(x => x.Items)
                .Include(x => x.Shipping)
                .ToListAsync();
        });
    }
    public async Task<Order?> GetById(Guid orderId)
    {
        var key = $"order={orderId}";

        return await _memoryCacheService.Get(key, async () =>
        {
           return await _context.Orders
           .Include(x => x.Items)
           .FirstOrDefaultAsync(x => x.Id == orderId);
        });
    }
}
