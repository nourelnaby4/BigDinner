using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Shippings;

namespace BigDinner.Domain.Models.Orders;

public class Order : AggregateRoot<Guid>
{
    private List<OrderItem> _items = new();

    public Guid OrderNumber { get; private set; }

    public DateTime OrderDateOnUtc { get; private set; }

    public Guid CustomerId { get; private set; }

    public OrderStatus OrderStatus { get; private set; }

    public Shipping Shipping { get; private set; }

    public IReadOnlyList<OrderItem> Items => _items.ToList();

    //public Payment Payment { get; private set; }

    private Order(Guid id) : base(id)
    {
    }

    private Order(Guid id, Guid orderNumer, Guid customerId) : base(id)
    {
        OrderNumber = orderNumer;
        CustomerId = customerId;
        OrderDateOnUtc = DateTime.UtcNow;
        OrderStatus = OrderStatus.Pending;

        this.RaiseDomainEvent(new OrderCreateDomainEvent(new OrderCreateEventMessage(CustomerId)));
    }

    public static Order Create(Guid customerId)
    {
        return new Order(Guid.NewGuid(), Guid.NewGuid(), customerId);
    }

    public void addOrderItem(OrderItem item)
    {
        _items.Add(item);
    }

    public void ChangeOrderStatus(OrderStatus status)
    {
        OrderStatus = status;
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
