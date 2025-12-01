using System.Text.Json;

namespace Microservice_Net9_.Basket.Api.Features.Basket.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(BasketService basketService) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {

            var basketAsJson = await basketService.GetBasketJsonFromCacheAsync(cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("Basket could not found", HttpStatusCode.NotFound);
            }

            Data.Basket currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

            if (!currentBasket.BasketItems.Any())
            {
                return ServiceResult.Error("Basket item not found.", HttpStatusCode.NotFound);
            }

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
                
            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
