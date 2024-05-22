using BigDinner.Domain.Models.Customers;

namespace BigDinner.Persistence.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IRedisCacheService _cache;

    public CustomerRepository(ApplicationDbContext context, IRedisCacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public void Add(Customer customer)
    {
        _context.Add(customer);
        _cache.Invalidate("customers");
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        string key = "customers";

        return await _cache.Get(key, async () =>
        {
            return await _context.Customers.ToListAsync();
        });
    }

    public async Task<Customer?> GetByIdAsync(Guid customerId)
    {
        string key = $"customers-{customerId}";

        return await _cache.Get(key, async () =>
        {
            return await _context.Customers.FindAsync(customerId);
        });
    }

    public void Update(Customer customer)
    {
        _context.Update(customer);
        _cache.Invalidate("customers");
        _cache.Invalidate($"customers-{customer.Id}");
    }
}
