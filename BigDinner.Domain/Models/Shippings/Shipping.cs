using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Orders;
using BigDinner.Domain.Models.Shippings.Events;
using Newtonsoft.Json;

namespace BigDinner.Domain.Models.Shippings;

public sealed class Shipping : AggregateRoot<Guid>
{
    public Guid OrderId { get; private set; }

    public Address Address { get; private set; }

    public Guid ShippingMethodId { get; private set; }

    public ShippingStatus Status { get; private set; }

    public Guid TrackingNumber { get; private set; }

    public  Order Order { get; private set; }

    public ShippingMethod ShippingMethod { get; private set; }

    [JsonConstructor]
    private Shipping(Guid id) : base(id)
    {
    }

    private Shipping(Guid id,Guid orderId,Guid shippingMethodId,Address address,Guid  trackingNumber) : base(id)
    {
        OrderId = orderId;
        Address = address;
        TrackingNumber = trackingNumber ;
        ShippingMethodId=shippingMethodId;
        Status = ShippingStatus.Inprogress;
    }

    public static Shipping Create( Guid orderId, Guid shippingMethodId , Address address)
    {
        return new Shipping(Guid.NewGuid(), orderId,shippingMethodId, address, Guid.NewGuid());
    }

    public void ChangeShippingStatus(ShippingStatus shippingStatus)
    {
        Status = shippingStatus;

        RaiseDomainEvent(new ChangeShippingStatusEvent(new ChangeShippingStatusEventMessage(Id,shippingStatus)));
    }
}

