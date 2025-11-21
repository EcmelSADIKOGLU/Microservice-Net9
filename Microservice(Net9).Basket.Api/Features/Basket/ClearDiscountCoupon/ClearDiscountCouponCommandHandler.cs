
using Microservice_Net9_.Basket.Api.Const;
using Microservice_Net9_.Shared.Services;
using System.Text.Json;

namespace Microservice_Net9_.Basket.Api.Features.Basket.ClearDiscountCoupon
{
    public class ClearDiscountCouponCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<ClearDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ClearDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            Guid userId = identityService.GetUserId;
            var casheKey = string.Format(BasketConst.BasketCacheKey, userId); 
            var basketAsJson = await distributedCache.GetStringAsync(casheKey, cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("Basket could not found", $"There is no basket with {userId} userId.", HttpStatusCode.NotFound);
            }

            Data.Basket currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

            if(!currentBasket.IsApplyDiscount)
            {
                return ServiceResult.Error("Discount could not found", $"There is no applied discount", HttpStatusCode.NotFound);
            }

            currentBasket.ClearDiscount();
            basketAsJson = JsonSerializer.Serialize(currentBasket);
            await distributedCache.SetStringAsync(casheKey, basketAsJson, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
