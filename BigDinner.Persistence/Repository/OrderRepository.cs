using BigDinner.Application.Common.Abstractions.Cache;
using BigDinner.Application.Common.Models;
using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Orders;
using BigDinner.Persistence.Extensions;

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

    public async Task AddAsync(Order order)
    {
        await _context.AddAsync(order);

        _cache.Invalidate(key).Wait();
    }


    public async Task<IEnumerable<Order>> GetAsync()
    {
        return await _cache.Get(key, async () =>
        {
            return await _context.Orders
                .Include(x => x.Items)
                .Include(x => x.Shipping)
                .AsSplitQuery()
                .OrderByDescending(x => x.OrderDateOnUtc)
                .ToListAsync();
        });
    }

    public async Task<PaginatedResult<Order>> GetPaginationList(int pageNumber = 1, int pageSize = 10)
    {

        return await _context.Orders
            .Include(x => x.Items)
            .Include(x => x.Shipping)
            .AsSplitQuery()
            .OrderByDescending(x => x.OrderDateOnUtc)
            .ToPaginatedListAsync(pageNumber, pageSize);
    }

    public async Task<Order?> GetByIdAsync(Guid orderId)
    {
        return await _cache.Get($"{key}-{orderId}", async () =>
        {
            return await _context.Orders
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == orderId);
        });
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Update(order);

        _cache.Invalidate(key).Wait();

        _cache.Invalidate($"{key}-{order.Id}").Wait();

        await Task.CompletedTask;
    }
}
