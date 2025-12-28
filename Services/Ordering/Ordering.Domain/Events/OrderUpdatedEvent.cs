namespace Ordering.Domain.Events;

internal record OrderUpdatedEvent(Order order) : IDomainEvent;

