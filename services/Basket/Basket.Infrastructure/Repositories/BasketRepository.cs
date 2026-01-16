using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }
        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await  _redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
            {
                return null!;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket)!;
        }

        public Task<ShoppingCart> UpdateBasket(ShoppingCart cart)
        {
            throw new NotImplementedException();
        }
        public Task DeleteBasket(string userName)
        {
            throw new NotImplementedException();
        }

    }
}
