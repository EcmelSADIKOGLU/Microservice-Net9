
using Microservice_Net9_.Basket.Api.Const;
using Microservice_Net9_.Basket.Api.Dto;
using Microservice_Net9_.Shared.Services;
using System.Text.Json;

namespace Microservice_Net9_.Basket.Api.Features.Basket.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {

            Guid userId = identityService.GetUserId;

            var casheKey = string.Format(BasketConst.BasketCacheKey, userId); //0 yazan yere userId gelecek
            var basketAsJson = await distributedCache.GetStringAsync(casheKey, cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("Basket could not found", $"There is no basket with {userId} userId.", HttpStatusCode.NotFound);
            }

            Data.Basket currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

            if (currentBasket.IsApplyDiscount)
            {
                if (currentBasket.CouponCode != request.CouponCode)
                {
                    currentBasket.ClearDiscount();
                    currentBasket.ApplyNewDiscount(request.CouponCode, request.DiscountRate);
                }

                currentBasket.ApplyExistingDiscount();
            }

            currentBasket.ApplyNewDiscount(request.CouponCode, request.DiscountRate);

            basketAsJson = JsonSerializer.Serialize(currentBasket);
            await distributedCache.SetStringAsync(casheKey, basketAsJson, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
