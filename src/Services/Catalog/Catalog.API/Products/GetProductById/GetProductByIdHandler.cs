using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

internal class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdHandler called {@Query}", query);
        var product = await session.Query<Product>().Where(x=>x.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
        var result = product.Adapt<GetProductByIdResult>();
        return result;
    }
}
