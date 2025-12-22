
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/category/{category}", async (string category, ISender sender)=>{

            var result = await sender.Send(new GetProductByCategoryQuery(category));

            var respontse =  result.Adapt<GetProductByCategoryResponse>();

            return Results.Ok(respontse);
        })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by Category")
            .WithDescription("get product by category");
    }
}
