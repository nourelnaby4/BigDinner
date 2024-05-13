using BigDinner.Domain.Models.Shippings;
using Microsoft.EntityFrameworkCore;

namespace BigDinner.Persistence.Repository;

public class ShippingMethodRepository : IShippingMethodRepository
{
    private readonly ApplicationDbContext _context;

    public ShippingMethodRepository(ApplicationDbContext context)
        => _context = context;

    public void Add(ShippingMethod shippingMethod)
        => _context.Add(shippingMethod);

    public async Task<List<ShippingMethod>> GetAll()
        => await _context.ShippingMethod.ToListAsync();
}
