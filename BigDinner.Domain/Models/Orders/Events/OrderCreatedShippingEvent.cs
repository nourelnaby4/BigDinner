using BigDinner.Domain.Models.Base;

namespace BigDinner.Domain.Models.Orders.Events;

public record OrderCreatedShippingEventMessage(Guid orderId,Guid customerId, Address address,Guid shippingMethodId);

public record OrderCreatedShippingEvent(OrderCreatedShippingEventMessage EventMessage ) : IDomainEvent;

