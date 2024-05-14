namespace BigDinner.Domain.Models.Shippings;

public interface IShippingRepository
{
    void Add(Shipping shipping);

    Task<List<Shipping>> GetAll();

    Task<Shipping> GetByIdAsync(Guid id);

    void Update(Shipping shipping);
}
