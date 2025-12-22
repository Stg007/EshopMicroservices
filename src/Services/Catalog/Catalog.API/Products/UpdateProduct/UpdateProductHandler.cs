
using Catalog.API.Exceptions;
using Catalog.API.Products.CreateProduct;

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

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200).WithMessage("Name required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category required");
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000).WithMessage("Description required");
        RuleFor(x => x.ImageFile).NotEmpty().MaximumLength(500).WithMessage("ImageFile required");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Price required").GreaterThan(0).WithMessage("must be greater than zero");
    }
}
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
