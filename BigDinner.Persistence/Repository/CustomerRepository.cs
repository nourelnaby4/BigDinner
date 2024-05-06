using BigDinner.Domain.Models.Customers;

namespace BigDinner.Persistence.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Customer customer)
    {
        _context.Add(customer);
    }

    public async Task<IEnumerable<Customer>> GetAll()
    {
       return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetById(Guid customerId)
    {
        return await _context.Customers.FindAsync(customerId);
    }
}
