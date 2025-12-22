


namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResponse(Guid Id,string Name, string Description, decimal Price, List<string> Category, string ImageFile);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:Guid}", async (ISender sender, Guid id) =>
        {
            var query = new GetProductByIdQuery(id);
            GetProductByIdResult result = await sender.Send(query);
            GetProductByIdResponse response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by Id")
            .WithDescription("get product by id");
    }
}
