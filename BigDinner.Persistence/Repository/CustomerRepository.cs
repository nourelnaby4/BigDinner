using BigDinner.Domain.Models.Customers;

namespace BigDinner.Persistence.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IRedisCacheService _cache;
    private const string key = "customers";

    public CustomerRepository(ApplicationDbContext context, IRedisCacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public void Add(Customer customer)
    {
        _context.Add(customer);
        _cache.Invalidate(key);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _cache.Get(key, async () =>
        {
            return await _context.Customers.ToListAsync();
        });
    }

    public async Task<Customer?> GetByIdAsync(Guid customerId)
    {

        return await _cache.Get($"{key}-{customerId}", async () =>
        {
            return await _context.Customers.FindAsync(customerId);
        });
    }

    public void Update(Customer customer)
    {
        _context.Update(customer);
        _cache.Invalidate(key);
        _cache.Invalidate($"{key}-{customer.Id}");
    }
}
