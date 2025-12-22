
namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductRequest(Guid Id);


public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteProductCommand(id);
            var result = await sender.Send(command);
            return Results.Ok("Deleted");
        })
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product")
            .WithDescription("Delete a product by Id");
    }
}
