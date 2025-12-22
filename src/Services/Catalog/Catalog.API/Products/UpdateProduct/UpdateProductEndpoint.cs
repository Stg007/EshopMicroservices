

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest
(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageFile,
    List<string> Category
);

public record UpdateProductResponse(Guid Id);


public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();

            return Results.Ok(response);
            
        }).WithName("Update Product")
            .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("update Product")
            .WithDescription("update a product");
    }
}
