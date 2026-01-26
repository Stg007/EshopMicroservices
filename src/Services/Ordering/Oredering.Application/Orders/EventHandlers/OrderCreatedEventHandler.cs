
namespace Oredering.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedDomainEvent>
{
    public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("OrderCreatedEventHandler handled for Order Id: {OrderId}", notification.Order.Id);
        return Task.CompletedTask;
    }
}
