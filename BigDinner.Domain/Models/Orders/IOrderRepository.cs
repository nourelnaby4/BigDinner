using BigDinner.Domain.Models.Menus;

namespace BigDinner.Domain.Models.Orders;

public interface IOrderRepository
{
    Task AddAsync(Order order);

    Task<IEnumerable<Order>> GetAsync();

    Task<Order?> GetByIdAsync(Guid orderId);

    Task UpdateAsync(Order order);

    Task<PaginatedResult<Order>> GetPaginationList(int pageNumber = 1, int pageSize = 10);
}
