namespace BigDinner.Domain.Models.Shippings;

public interface IShippingRepository
{
    void Add(Shipping shipping);

    Task<List<Shipping>> GetAll();
}
