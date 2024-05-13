namespace BigDinner.Domain.Models.Shippings;

public interface IShippingMethodRepository
{
    void Add(ShippingMethod shippingMethod);

    Task<List<ShippingMethod>> GetAll();
}
