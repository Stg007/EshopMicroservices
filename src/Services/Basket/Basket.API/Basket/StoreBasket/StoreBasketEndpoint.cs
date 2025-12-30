
namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);

public record StoreBasketResponse(string UserName);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<StoreBasketCommand>();
            var result = await sender.Send(command, cancellationToken);
            var response = result.Adapt<StoreBasketResponse>();
            return Results.Ok(response);
        })
            .WithName("StoreBasket")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store Basket")
            .WithDescription("Store Basket");

        app.MapPut("/basket", async (StoreBasketRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<StoreBasketCommand>();
            var result = await sender.Send(command, cancellationToken);
            var response = result.Adapt<StoreBasketResponse>();
            return Results.Ok(response);
        })
            .WithName("UpdateBasket")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Basket")
            .WithDescription("Update Basket");
    }
}
