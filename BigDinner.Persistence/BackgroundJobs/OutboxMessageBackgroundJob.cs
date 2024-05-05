using BigDinner.Domain.Models.Base;
using BigDinner.Persistence.OutboxMessages;
using MediatR;
using Newtonsoft.Json;
using Quartz;

namespace BigDinner.Persistence.BackgroundJobs;

[DisallowConcurrentExecution] // prevent multible instance created for background job
public class OutboxMessageBackgroundJob : IJob
{
    private readonly ApplicationDbContext _context;

    private readonly IPublisher _publisher;

    public OutboxMessageBackgroundJob(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            List<OutboxMessage> messages = await _context
           .Set<OutboxMessage>()
           .Where(x => x.ProcessedOnUtc == null)
           .Take(20)
           .ToListAsync(context.CancellationToken);

            foreach (var outboxMessage in messages)
            {
                IDomainEvent? domainEvent = JsonConvert
                    .DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

                if (domainEvent is null)
                {
                    continue;
                }

                await _publisher.Publish(domainEvent, context.CancellationToken);

                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
