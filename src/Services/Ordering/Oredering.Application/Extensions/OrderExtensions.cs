using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oredering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        List<OrderDto> orderDtos = new();
        foreach (var order in orders)
        {
            var orderDto = new OrderDto(
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                Status: order.Status,
                BillingAddress: new AddressDto(
                    EmailAdress: order.BillingAddres.EmailAddress,
                    FirstName: order.BillingAddres.FirstName,
                    LastName: order.BillingAddres.LastName,
                    AdressLine: order.BillingAddres.AdressLine,
                    State: order.BillingAddres.State,
                    ZipCode: order.BillingAddres.ZipCode,
                    Country: order.BillingAddres.Country
                ),
                Payment: new PaymentDto(
                    CardNumber: order.Payment.CardNumber,
                    CardName:  order.Payment.CardNumber,
                    Expiration: order.Payment.Expiration,
                    CVV: order.Payment.CVV,
                    PaymentMethod: order.Payment.PaymentMethod
                ),
                ShippingAddress: new AddressDto(
                    EmailAdress: order.BillingAddres.EmailAddress,
                    FirstName: order.BillingAddres.FirstName,
                    LastName: order.BillingAddres.LastName,
                    AdressLine: order.ShippingAddres.AdressLine,
                    State: order.ShippingAddres.State,
                    ZipCode: order.ShippingAddres.ZipCode,
                    Country: order.ShippingAddres.Country
                ),
                OrderName: order.OrderName.Value,
                Items: order.orderItems.Select(oi => new OrderItemDto(
                    OrderId: oi.Id.Value,
                    ProductId: oi.ProductId.Value,
                    Quantity: oi.Quantity,
                    Price: oi.Price
                )).ToList());
            
            orderDtos.Add(orderDto);
        }
        return orderDtos;
    }
}
