using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository _BasketRepository, IDistributedCache cache) 
        : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
        {
             await _BasketRepository.DeleteBasket(userName, cancellationToken);
             await cache.RemoveAsync(userName, cancellationToken);
             return true;
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
        {
            var cacheBasket = await cache.GetStringAsync(userName, cancellationToken);
            if(!string.IsNullOrEmpty(cacheBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket) ?? new ShoppingCart(userName); ;
            }
            var basket = await _BasketRepository.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken)
        {
            await _BasketRepository.StoreBasket(basket, cancellationToken);
            await cache.SetStringAsync(basket.UserName,JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }



    }
}
