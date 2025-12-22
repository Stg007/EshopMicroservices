using Catalog.API.Exceptions;
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Guid Id, string Name, string Description, decimal Price, List<string> Category, string ImageFile);

internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) 
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryHandler called {@Query}", query);
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if(product is null)
        {
            throw new ProductNotFoundException($"Product with Id {query.Id} not found.");
        }

        var result = new GetProductByIdResult(product.Id, product.Name, product.Description, product.Price,product.Category,product.ImageFile);

        return result;
    }
}
