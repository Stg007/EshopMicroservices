using Mapster;
using Ordering.Domain.ValueObjects;
using Oredering.Application.Data;

namespace Oredering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext context) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.order);
        context.Orders.Add(order);
        await context.SaveChangesAsync(cancellationToken);
        return new CreateOrderResult(order.Id.Value);
    }
    private Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName
            ,orderDto.ShippingAddress.EmailAdress,
            orderDto.ShippingAddress.AdressLine,orderDto.ShippingAddress.Country,orderDto.ShippingAddress.State
            ,orderDto.ShippingAddress.ZipCode);
        var BillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, 
            orderDto.BillingAddress.EmailAdress,
            orderDto.BillingAddress.AdressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State
            , orderDto.BillingAddress.ZipCode);

        var payment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber
            , orderDto.Payment.Expiration, orderDto.Payment.CVV, orderDto.Payment.PaymentMethod);


        var newOrder = Order.Create(
            id: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(orderDto.CustomerId),
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: shippingAddress,
            billingAddress: BillingAddress,
            payment: payment
            );

        foreach ( var orderItemDto in orderDto.Items)
        {
            newOrder.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);

        }
        return newOrder;
    }
}
