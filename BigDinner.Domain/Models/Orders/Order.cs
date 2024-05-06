using BigDinner.Domain.Models.Customers;

namespace BigDinner.Domain.Models.Orders;

public class Order : AggregateRoot<Guid>
{
    private List<OrderItem> _items = new();

    public string OrderNumber { get; private set; }

    public DateTime OrderDateOnUtc { get; private set; }

    public Guid CustomerId { get; private set; }

    public Customer Customer { get; private set; }

    public OrderStatus OrderStatus { get; private set; }

    public IReadOnlyList<OrderItem> Items => _items.ToList();

    //public Payment Payment { get; private set; }

    private Order(Guid id) : base(id)
    {
    }

    private Order(Guid id, string orderNumer, Guid customerId) : base(id)
    {
        OrderNumber = orderNumer;
        CustomerId = customerId;
        OrderDateOnUtc = DateTime.UtcNow;
        OrderStatus = OrderStatus.Pending;
    }

    public void addOrderItem(OrderItem item)
    {
        _items.Add(item);
    }

    public static Order Create(string orderNumer, Guid customerId)
    {
        return new Order(Guid.NewGuid(), orderNumer, customerId);
    }

    public Price CalculateTotalPrice()
    {
        decimal totalPrice = 0;

        foreach (var menuItem in _items)
        {
            totalPrice += menuItem.Price.Value;
        }
   
        return new Price(totalPrice);
    }
}
