using Microservice_Net9_.Basket.Api.Const;
using Microservice_Net9_.Basket.Api.Dto;
using Microservice_Net9_.Shared.Services;
using System.Text.Json;

namespace Microservice_Net9_.Basket.Api.Features.Basket.GetBasket
{
    public class GetBasketQueryHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            Guid userId = identityService.GetUserId;

            var casheKey = string.Format(BasketConst.BasketCacheKey, userId); //0 yazan yere userId gelecek
            var basketAsJson = await distributedCache.GetStringAsync(casheKey, cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult<BasketDto>.Error("Basket could not found", $"There is no basket with {userId} userId.", HttpStatusCode.NotFound);
            }

            BasketDto currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsJson)!;

            return ServiceResult<BasketDto>.SuccessAsOk(currentBasket);
        }
    }
}
