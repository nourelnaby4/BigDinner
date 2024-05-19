using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Shippings;
using BigDinner.Domain.Models.Orders.Events;
using Newtonsoft.Json;

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

    [JsonConstructor]
    private Order(Guid id) : base(id)
    {
    }

    private Order(Guid id, Guid orderNumer, Guid customerId, Guid shippingMethodId, Address? address) : base(id)
    {
        OrderNumber = orderNumer;
        CustomerId = customerId;
        OrderDateOnUtc = DateTime.UtcNow;
        OrderStatus = OrderStatus.Pending;

        this.RaiseDomainEvent(new OrderCreateDomainEvent(new OrderCreateSendMessageEventData(CustomerId)));

        this.RaiseDomainEvent(new OrderCreatedShippingEvent(new OrderCreatedShippingEventMessage(id, customerId, address, shippingMethodId)));
    }

    public static Order Create(Guid customerId, Guid shippingMethodId, Address? address)
    {
        return new Order(Guid.NewGuid(), Guid.NewGuid(), customerId, shippingMethodId, address);
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
