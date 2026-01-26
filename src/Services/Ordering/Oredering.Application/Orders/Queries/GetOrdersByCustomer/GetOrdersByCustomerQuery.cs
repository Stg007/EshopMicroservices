namespace Oredering.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetOrdersByCustomerResult>;
public record GetOrdersByCustomerResult(IEnumerable<OrderDto> orders);