

using Basket.API.Data;
using Discount.Grpc;

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

public class StoreBasketCommandHandler (IBasketRepository _repo, DiscountProtoService.DiscountProtoServiceClient discount)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        // TODO: communicate with Discount.Grpc and calculate the latest prices

        foreach (var item in command.Cart.Items)
        {
            var coupon = await discount.GetDiscountAsync(new GetDiscountRequest() { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= (decimal)coupon.Amount;
        }

        // Here you would typically store the shopping cart in a database or cache

        var basket = await _repo.StoreBasketAsync(command.Cart);

        return new StoreBasketResult(basket.UserName);
    }
}
