namespace Oredering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto order) :ICommand<UpdateOrderResult>;
public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.order.Id).NotEmpty().WithMessage("Id Is required");
        RuleFor(x => x.order.OrderName).NotEmpty().WithMessage("Name Is required");
        RuleFor(x => x.order.CustomerId).NotNull().WithMessage("Customer Id Is required");
    }
}
