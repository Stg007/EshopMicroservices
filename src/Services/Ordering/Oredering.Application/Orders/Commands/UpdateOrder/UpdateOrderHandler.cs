using Ordering.Domain.ValueObjects;
using Oredering.Application.Data;
using Oredering.Application.Exceptions;
using Oredering.Application.Orders.Commands.CreateOrder;

namespace Oredering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, 
        CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.order.Id);
        var order = await context.Orders.FindAsync([orderId],cancellationToken);
        if(order is null)
        {
            throw new OrderNotFoundException(command.order.Id);
        }
        UpdateOrderWithNewValues(order, command.order);

        context.Orders.Update(order);
        await context.SaveChangesAsync(cancellationToken);
        return new UpdateOrderResult(true);
    }

    public void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {

        var shippingAddress = Address.Of(
            orderDto.ShippingAddress.FirstName, 
            orderDto.ShippingAddress.LastName, 
            orderDto.ShippingAddress.EmailAdress,
            orderDto.ShippingAddress.AdressLine, 
            orderDto.ShippingAddress.Country, 
            orderDto.ShippingAddress.State, 
            orderDto.ShippingAddress.ZipCode);

        var BillingAddress = Address.Of(
            orderDto.BillingAddress.FirstName, 
            orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.EmailAdress,
            orderDto.BillingAddress.AdressLine, 
            orderDto.BillingAddress.Country, 
            orderDto.BillingAddress.State, 
            orderDto.BillingAddress.ZipCode);

        var updatedPayment = Payment.Of(
            orderDto.Payment.CardName,
            orderDto.Payment.CardNumber,
            orderDto.Payment.Expiration,
            orderDto.Payment.CVV,
            orderDto.Payment.PaymentMethod);

        // For simplicity, we will not update payment and items in this example.

        order.Update( OrderName.Of(orderDto.OrderName), shippingAddress
            , BillingAddress, updatedPayment, orderDto.Status);

    }
}
