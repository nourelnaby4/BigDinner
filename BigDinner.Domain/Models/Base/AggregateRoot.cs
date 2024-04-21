namespace BigDinner.Domain.Models.BaseModels;

public class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    protected AggregateRoot(TId id) : base(id)
    {
    }
}

