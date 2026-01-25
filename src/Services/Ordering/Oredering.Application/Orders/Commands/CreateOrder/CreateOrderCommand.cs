namespace Oredering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto order) 
    : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid OrderId);

public class CreateOrderCommandValidator 
    : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.order.OrderName)
            .NotEmpty().WithMessage("Order name is required.");
        RuleFor(c => c.order.CustomerId)
            .NotNull().WithMessage("Customer Id is required");
        RuleFor(c => c.order.Items)
            .NotEmpty().WithMessage("Order Items should be greater than zero");
    }
}