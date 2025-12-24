using Catalog.API.Exceptions;
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Guid Id, string Name, string Description, decimal Price, List<string> Category, string ImageFile);

internal class GetProductByIdQueryHandler(IDocumentSession session) 
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if(product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        var result = new GetProductByIdResult(product.Id, product.Name, product.Description, product.Price,product.Category,product.ImageFile);

        return result;
    }
}
