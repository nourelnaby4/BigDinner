using BigDinner.Domain.Models.Menus;

namespace BigDinner.Domain.Models.Orders;

public interface IOrderRepository
{
    void Add(Order order);

    Task<IEnumerable<Order>> GetAll();

    Task<Order?> GetById(Guid orderId);
}
