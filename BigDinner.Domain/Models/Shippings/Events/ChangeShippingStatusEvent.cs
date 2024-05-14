using BigDinner.Domain.Models.Base;

namespace BigDinner.Domain.Models.Shippings.Events;

public record ChangeShippingStatusEventMessage( Guid shippingId,ShippingStatus Status);

public record ChangeShippingStatusEvent(ChangeShippingStatusEventMessage eventMessages) : IDomainEvent;

