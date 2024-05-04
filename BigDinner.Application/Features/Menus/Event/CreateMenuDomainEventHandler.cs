using BigDinner.Domain.Models.Menus;
using Serilog;

namespace BigDinner.Application.Features.Menus.Event;

public class CreateMenuDomainEventHandler
    : INotificationHandler<CreateMenuDomainEvent>
{
    private readonly ILogger _logger;

    public CreateMenuDomainEventHandler(ILogger logger)
        => _logger = logger;

    public Task Handle(CreateMenuDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.Information("New menu created. MenuId: {MenuId}, Name: {MenuName}, Description: {MenuDescription}",
                notification.Menu.Id, notification.Menu.Name, notification.Menu.Description);

        return Task.CompletedTask;
    }
}
