using BigDinner.Domain.Models.Base;

namespace BigDinner.Domain.Models.Menus;

public record MenuCreated(Menu Menu) : IDomainEvent;

