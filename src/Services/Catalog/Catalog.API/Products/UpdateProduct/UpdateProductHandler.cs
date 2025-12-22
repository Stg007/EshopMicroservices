
using Catalog.API.Exceptions;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand
(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageFile,
    List<string> Category
): ICommand<UpdateProductResult>;

public record UpdateProductResult(Guid Id);

internal class UpdateProductHandler(IDocumentSession session) 
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var productToUpdate = await session.Query<Product>()
            .Where(p => p.Id == command.Id)
            .SingleOrDefaultAsync(cancellationToken) ?? throw new ProductNotFoundException();

        productToUpdate.Name = command.Name;
        productToUpdate.Description = command.Description;
        productToUpdate.Price = command.Price;
        productToUpdate.ImageFile = command.ImageFile;
        productToUpdate.Category = command.Category;

        session.Update(productToUpdate);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(productToUpdate.Id);
    }
}
