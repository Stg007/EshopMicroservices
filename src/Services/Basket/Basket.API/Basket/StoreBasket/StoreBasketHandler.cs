

using Basket.API.Data;

namespace Basket.API.Basket.StoreBasket;
public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull();
        RuleFor(x => x.Cart.UserName).NotEmpty();
        RuleFor(x => x.Cart.Items).NotNull();
    }
}

public class StoreBasketCommandHandler (IBasketRepository _repo)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;
        // Here you would typically store the shopping cart in a database or cache

        var basket = await _repo.StoreBasketAsync(cart);

        return new StoreBasketResult(basket.UserName);
    }
}
