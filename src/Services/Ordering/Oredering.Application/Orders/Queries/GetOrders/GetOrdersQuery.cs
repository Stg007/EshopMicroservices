using BuildingBlocks.Pagination;

namespace Oredering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest pr) : IQuery<GetOrderResult>;
public record GetOrderResult(PaginatedResult<OrderDto> orders);
