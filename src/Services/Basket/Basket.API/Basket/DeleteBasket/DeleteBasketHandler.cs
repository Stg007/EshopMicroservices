
using Basket.API.Data;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool isSuccess);

public class DeleteBasketCommandHandler (IBasketRepository _repo)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        var state = await _repo.DeleteBasketAsync(command.UserName, cancellationToken);
        return new DeleteBasketResult(state);
    }
}
