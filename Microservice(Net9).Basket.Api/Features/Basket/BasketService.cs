using Microservice_Net9_.Basket.Api.Const;
using Microservice_Net9_.Shared.Services;
using System.Text.Json;

namespace Microservice_Net9_.Basket.Api.Features.Basket
{
    public class BasketService(IDistributedCache distributedCache, IIdentityService identityService)
    {
        //0 yazan yere userId gelecek
        private string GetCacheKey() => string.Format(BasketConst.BasketCacheKey, identityService.GetUserId); 

        public Task<string?> GetBasketJsonFromCacheAsync(CancellationToken cancellationToken)
        {
            var cacheKey = GetCacheKey();
            var basketAsJson = distributedCache.GetStringAsync(cacheKey, cancellationToken);

            return basketAsJson;

        }

        public async Task CreateBasketCashAsync(Data.Basket basketDto, CancellationToken cancellationToken)
        {
            var cacheKey = GetCacheKey();
            var basketAsString = JsonSerializer.Serialize(basketDto);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
        }
    }
}
