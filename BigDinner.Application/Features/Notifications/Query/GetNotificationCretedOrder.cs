namespace BigDinner.Application.Features.Notifications.Query;

public record GetNotificationCretedOrderQuery()
    : IRequest<Response<IEnumerable<GetNotificationCretedOrderQueryResponse>>>;

public record GetNotificationCretedOrderQueryResponse(Guid CustomerId, string Title, string Message, DateTimeOffset CreatedAt, bool IsRead);