
namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200).WithMessage("Name required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category required");
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000).WithMessage("Description required");
        RuleFor(x => x.ImageFile).NotEmpty().MaximumLength(500).WithMessage("ImageFile required");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Price required").GreaterThan(0).WithMessage("must be greater than zero");
    }
}
internal class CreateProductCommandHanlder (IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Business logic to create a product would go here.

        var product = new Product()
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // save to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        // return createProductResult result
        return new CreateProductResult(product.Id);
    }
}
