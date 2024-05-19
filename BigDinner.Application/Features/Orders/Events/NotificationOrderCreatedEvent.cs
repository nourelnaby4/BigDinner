using BigDinner.Application.Common.Abstractions.Cache;
using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Notifications;
using BigDinner.Domain.Models.Orders.Events;

namespace BigDinner.Application.Features.Orders.Events;

public class NotificationOrderCreatedEvent : INotificationHandler<OrderCreatedNoticationEvent>
{
    private readonly IRedisCacheService _cache;

    public NotificationOrderCreatedEvent(IRedisCacheService cache)
    {
        _cache = cache;
    }

    public async Task Handle(OrderCreatedNoticationEvent notification, CancellationToken cancellationToken)
    {
        var notify = Notification.Create(notification.Order.CustomerId, $"{notification.Order.OrderNumber} is created", "order in progress of shipping");

        var cacheKey = $"{notification.Order.CustomerId}{notification.Order.Id}";

        var list = new List<Notification>();
        list.Add(notify);

        await _cache.AddTolist<Notification>("notifications",list);
    }
}
