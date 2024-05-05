using BigDinner.Domain.Models.Menus;
using Serilog;

namespace BigDinner.Application.Features.Menus.Event;

public class CreateMenuDomainEventHandler
    : INotificationHandler<MenuCreateDomainEvent>
{


    public Task Handle(MenuCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        Log.Information("New menu created. MenuId: {MenuId}, Name: {MenuName}",
                notification.Menu.id, notification.Menu.name);

        return Task.CompletedTask;
    }
}
