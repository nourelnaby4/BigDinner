using BigDinner.Domain.Models.Shippings;

namespace BigDinner.Persistence.Repository;

public class ShippingRepository : IShippingRepository
{
    private readonly ApplicationDbContext _context;

    public ShippingRepository(ApplicationDbContext context)
        => _context = context;

    public void Add(Shipping shipping)
        => _context.Add(shipping);

    public async Task<List<Shipping>> GetAll()
        => await _context.Shipping.ToListAsync();
}
