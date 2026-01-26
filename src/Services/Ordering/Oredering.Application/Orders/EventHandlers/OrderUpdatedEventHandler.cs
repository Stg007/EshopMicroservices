namespace Oredering.Application.Orders.EventHandlers;

public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger)
    : INotificationHandler<OrderUpdatedDomainEvent>
{
    public Task Handle(OrderUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order with Id: {OrderId} has been updated.", notification.Order.Id);
        return Task.CompletedTask;
    }
}
