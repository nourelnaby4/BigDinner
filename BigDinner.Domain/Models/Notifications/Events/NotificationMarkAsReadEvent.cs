namespace BigDinner.Domain.Models.Notifications.Events;

public record NotificationMarkAsReadEvent(Notification eventMessage) : IDomainEvent;

