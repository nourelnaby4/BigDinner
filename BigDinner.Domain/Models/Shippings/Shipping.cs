using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Orders;

namespace BigDinner.Domain.Models.Shippings;

public sealed class Shipping : AggregateRoot<Guid>
{
    public Guid Id { get;private set; }

    public Guid OrderId { get; private set; }

    public Address Address { get; private set; }

    public Guid ShippingMethodId { get; private set; }

    public ShippingStatus Status { get; private set; }

    public Guid TrackingNumber { get; private set; }

    public Order Order { get; private set; }

    private Shipping(Guid id) : base(id)
    {
    }

    private Shipping(Guid id,Guid orderId,Guid shippingMethodId, Guid trackingNumer,Address address) : base(id)
    {
        OrderId = orderId;
        Address = address;
        TrackingNumber = trackingNumer;
        ShippingMethodId=shippingMethodId;
        Status = ShippingStatus.Inprogress;
    }

    public static Shipping Create( Guid orderId, Guid shippingMethodId, Guid trackingNumer, Address address)
    {
        var Id= Guid.NewGuid();
        return new Shipping(Id, orderId,shippingMethodId,trackingNumer, address);
    }
}

