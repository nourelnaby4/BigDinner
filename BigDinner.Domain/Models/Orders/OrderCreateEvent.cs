using BigDinner.Domain.Models.Base;

namespace BigDinner.Domain.Models.Orders;

public record OrderCreateEventMessage(Guid customerId);

public record OrderCreateDomainEvent(OrderCreateEventMessage order) : IDomainEvent;