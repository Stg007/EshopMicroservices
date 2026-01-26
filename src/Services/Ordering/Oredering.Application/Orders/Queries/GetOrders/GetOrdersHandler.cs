
using BuildingBlocks.Pagination;

namespace Oredering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersQuery, GetOrderResult>
{
    public async Task<GetOrderResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.pr.PageIndex;
        var pageSize = query.pr.PageSize;
        var totalCount = await context.Orders.LongCountAsync(cancellationToken);

        var orders = await context.Orders
            .Include(o => o.orderItems)
            .OrderBy(o => o.OrderName.Value)
            .Skip(pageSize*pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetOrderResult(new PaginatedResult<OrderDto>(
            pageIndex, pageSize, totalCount, orders.ToOrderDtoList()));
    }
}
