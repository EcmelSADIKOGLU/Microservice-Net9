using System.Text.Json;

namespace Microservice_Net9_.Basket.Api.Features.Basket.ClearDiscountCoupon
{
    public class ClearDiscountCouponCommandHandler(BasketService basketService) : IRequestHandler<ClearDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ClearDiscountCouponCommand request, CancellationToken cancellationToken)
        {

            var basketAsJson = await basketService.GetBasketJsonFromCacheAsync(cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("Basket could not found", HttpStatusCode.NotFound);
            }

            Data.Basket currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

            if(!currentBasket.IsApplyDiscount)
            {
                return ServiceResult.Error("Discount could not found", $"There is no applied discount", HttpStatusCode.NotFound);
            }

            currentBasket.ClearDiscount();
            await basketService.CreateBasketCashAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
