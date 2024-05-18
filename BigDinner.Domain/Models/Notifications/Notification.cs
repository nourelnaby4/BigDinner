using BigDinner.Domain.Models.Notifications.Events;

namespace BigDinner.Domain.Models.Notifications;

public class Notification : AggregateRoot<Guid>
{
    public Guid CustomerId { get; private set; }
    public string Title { get; private set; }
    public string Message { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public bool IsRead { get; private set; }

    private Notification(Guid id) : base(id) { }

    private Notification(Guid id, Guid customerId, string title, string message) : base(id)
    {
        CustomerId = customerId;
        Title = title;
        Message = message;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public static Notification Create(Guid customerId, string title, string message)
    {
        return new Notification(Guid.NewGuid(), customerId, title, message);
    }

    public void MarkAsRead()
    {
        IsRead = true;

        RaiseDomainEvent(new NotificationMarkAsReadEvent(this));
    }
}
