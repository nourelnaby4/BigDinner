using BigDinner.Domain.Models.Notifications;

namespace BigDinner.Domain.Models.Orders.Events;

public record OrderCreatedNoticationEvent(Order Order) : IDomainEvent;

