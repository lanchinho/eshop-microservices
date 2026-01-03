using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler
    (IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
        var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();

        if (await featureManager.IsEnabledAsync("OrderFullfilment"))
            await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
    }
}
