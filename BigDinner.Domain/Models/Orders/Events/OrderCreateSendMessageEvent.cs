using BigDinner.Domain.Models.Base;

namespace BigDinner.Domain.Models.Orders.Events;

public record OrderCreateSendMessageEventData(Guid customerId);

public record OrderCreateDomainEvent(OrderCreateSendMessageEventData order) : IDomainEvent;

