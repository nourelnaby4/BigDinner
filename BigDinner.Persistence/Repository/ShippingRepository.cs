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
        => await _context.Shipping
          .Include(x=>x.ShippingMethod)
          .ToListAsync();

    public async Task<Shipping> GetByIdAsync(Guid id)
        => await _context.Shipping
        .Include(x => x.Order)
        .Include(x=>x.Address)
        .SingleOrDefaultAsync(x => x.Id == id);

    public void Update(Shipping shipping)
        => _context.Update(shipping);
}
