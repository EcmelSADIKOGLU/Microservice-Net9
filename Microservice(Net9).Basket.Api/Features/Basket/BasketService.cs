using Microservice_Net9_.Basket.Api.Const;
using Microservice_Net9_.Shared.Services;
using System.Text.Json;

namespace Microservice_Net9_.Basket.Api.Features.Basket
{
    public class BasketService(IDistributedCache distributedCache, IIdentityService identityService)
    {
        //0 yazan yere userId gelecek
        private string GetCacheKey()
        {
            return string.Format(BasketConst.BasketCacheKey, identityService.UserId);
        }

        private string GetCacheKey(Guid userId)
        {
            return string.Format(BasketConst.BasketCacheKey, userId);
        }

        public Task<string?> GetBasketJsonFromCacheAsync(CancellationToken cancellationToken)
        {
            return distributedCache.GetStringAsync(GetCacheKey(), cancellationToken);
        }

        public async Task CreateBasketCacheAsync(Data.Basket basket, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(GetCacheKey(), basketAsString, cancellationToken);
        }

        public async Task DeleteBasket(Guid userId)
        {
            await distributedCache.RemoveAsync(GetCacheKey(userId));
        }
    }
}
