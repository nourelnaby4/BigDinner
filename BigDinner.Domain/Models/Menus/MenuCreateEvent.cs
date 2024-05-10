using BigDinner.Domain.Models.Base;

namespace BigDinner.Domain.Models.Menus;

public record MenuCreateEventMessage(Guid id, string name);

public record MenuCreateDomainEvent(MenuCreateEventMessage Menu) : IDomainEvent;
