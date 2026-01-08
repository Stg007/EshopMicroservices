
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository repo, IDistributedCache cache) 
    : IBasketRepository
{
    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var result = await repo.DeleteBasketAsync(userName, cancellationToken);
        if(result)
        {
            await cache.RemoveAsync(userName, cancellationToken);
        }

        return result;
    }

    public async Task<ShoppingCart?> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
        if(!string.IsNullOrEmpty(cachedBasket))
        {
            return System.Text.Json.JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
        }
        var basket = await repo.GetBasketAsync(userName, cancellationToken);
        await cache.SetStringAsync(userName, System.Text.Json.JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        var storedBasket = await repo.StoreBasketAsync(basket, cancellationToken);
        await cache.SetStringAsync(basket.UserName, System.Text.Json.JsonSerializer.Serialize(storedBasket), cancellationToken);
        return storedBasket;
    }
}
