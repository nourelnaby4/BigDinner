using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Orders;

namespace BigDinner.Persistence.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
        => _context = context;


    public void Add(Order order)
        => _context.Add(order);


    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _context.Orders
            .Include(x => x.Items)
            .Include(x => x.Shipping)
            .ToListAsync();
    }

    public async Task<Order?> GetById(Guid orderId)
    {
        return await _context.Orders
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == orderId);
    }
}
