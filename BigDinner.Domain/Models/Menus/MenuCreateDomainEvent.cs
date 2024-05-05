using BigDinner.Domain.Models.Base;

namespace BigDinner.Domain.Models.Menus;

public record MenuCreateDomainEvent(MenuCreateEventMessage Menu) : IDomainEvent;

