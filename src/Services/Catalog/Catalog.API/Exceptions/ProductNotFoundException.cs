namespace Catalog.API.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException() : base("Product not found")
    {

    }
    public ProductNotFoundException(string msg) : base(msg)
    {

    }
}
