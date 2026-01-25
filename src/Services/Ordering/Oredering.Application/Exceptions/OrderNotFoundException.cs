using BuildingBlocks.Exceptions;

namespace Oredering.Application.Exceptions;

public class OrderNotFoundException: NotFoundException
{
    public OrderNotFoundException(Guid IdOrder):base("Order "+IdOrder.ToString()+" not found")
    {
        
    }
}
