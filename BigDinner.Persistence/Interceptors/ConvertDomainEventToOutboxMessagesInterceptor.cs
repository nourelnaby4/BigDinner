using BigDinner.Domain.Models.Base;
using BigDinner.Domain.Models.BaseModels;
using BigDinner.Domain.Models.Menus;
using BigDinner.Persistence.OutboxMessages;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Threading;

namespace BigDinner.Persistence.Interceptors;

public sealed class ConvertDomainEventToOutboxMessagesInterceptor
    : SaveChangesInterceptor
{
    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        await PublishDomainEvents(eventData?.Context);
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    public override  InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
         PublishDomainEvents(eventData?.Context).GetAwaiter().GetResult();
        return  base.SavingChanges(eventData, result);
    }


    private async Task PublishDomainEvents(DbContext? dbContext)
    {
        if (dbContext is null)
        {
            return;
        }

        var outboxMessages = dbContext.ChangeTracker
             .Entries<IHasDomainEvents>()
             .Select(x => x.Entity)
             .SelectMany(aggregateRoot =>
             {
                 var domainEvents = aggregateRoot.GetDomainEvents();

                 aggregateRoot.ClearDomainEvents();

                 return domainEvents;
             })
             .Select(domainEvent => new OutboxMessage
             {
                 Id = Guid.NewGuid(),
                 OccurredOnUtc = DateTime.UtcNow,
                 Type = domainEvent.GetType().Name,
                 Content = JsonConvert.SerializeObject(
                     domainEvent,
                     new JsonSerializerSettings
                     {
                         TypeNameHandling = TypeNameHandling.All,
                     })
             })
             .ToList();

        if (outboxMessages.Any())
        {
            dbContext.Set<OutboxMessage>().AddRange(outboxMessages);

           await dbContext.SaveChangesAsync();
        }

    }
}
