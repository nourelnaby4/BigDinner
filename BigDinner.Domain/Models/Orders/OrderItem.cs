using Newtonsoft.Json;

namespace BigDinner.Domain.Models.Orders;

public class OrderItem : Entity<Guid>
{
    [JsonConstructor]
    private OrderItem(Guid id) : base(id)
    {
    }

    public string Name { get; private set; }

    public int Quantity { get; private set; }

    public Price Price { get; private set; }

    public Guid OrderId { get; private set; }


    private OrderItem(Guid id, Guid orderId, string name, int quantity, Price price) : base(id)
    {
        
        OrderId = orderId;
        Name = name;
        Quantity = quantity;
        Price = price;
    }

    public static OrderItem Create(Guid orderId, string name, int quantity, Price price)
    {
        return new OrderItem(Guid.NewGuid(), orderId, name, quantity, price);
    }

    public void UpdateItem(string name, int quantity, Price price)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
    }

    public Price CalculateTotalPrice()
    {
        decimal totalPrice = 0;

        totalPrice = Quantity * Price.Value;

        return new Price(totalPrice);
    }

}
