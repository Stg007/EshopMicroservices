using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if(await session.Query<Product>().AnyAsync(cancellation))
            return;


        session.Store<Product>(GetPreconfigurationProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Product> GetPreconfigurationProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sample Product 1",
                Description = "This is a sample product.",
                Price = 19.99m,
                ImageFile = "product1.png",
                Category = new List<string> { "Category A", "Category B" }
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sample Product 2",
                Description = "This is a sample product.",
                Price = 19.99m,
                ImageFile = "product2.png",
                Category = new List<string> { "Category C", "Category B" }
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sample Product 3",
                Description = "This is a sample product.",
                Price = 19.99m,
                ImageFile = "product3.png",
                Category = new List<string> { "Category C", "Category D" }
            }
        };
    }
}
