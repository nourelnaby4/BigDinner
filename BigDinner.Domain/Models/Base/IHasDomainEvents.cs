namespace BigDinner.Domain.Models.Base;

public interface IHasDomainEvents
{
    public void RaiseDomainEvent(IDomainEvent domainEvent);

    public IReadOnlyList<IDomainEvent> GetDomainEvents();

    public void ClearDomainEvents();
}
