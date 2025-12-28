namespace Ordering.Domain.Events;

internal record OrderUpdatedEvent(Order Order) : IDomainEvent;

