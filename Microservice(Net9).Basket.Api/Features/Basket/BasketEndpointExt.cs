using Asp.Versioning.Builder;
using Microservice_Net9_.Basket.Api.Features.Basket.AddBasketItem;
using Microservice_Net9_.Basket.Api.Features.Basket.ApplyDiscountCoupon;
using Microservice_Net9_.Basket.Api.Features.Basket.ClearDiscountCoupon;
using Microservice_Net9_.Basket.Api.Features.Basket.DeleteBasketItem;
using Microservice_Net9_.Basket.Api.Features.Basket.GetBasket;

namespace Microservice_Net9_.Basket.Api.Features.Basket
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketItemGroupItem()
                .DeleteBasketItemGroupItem()
                .GetBasketGroupItem()
                .ApplyDiscountCouponGroupItem()
                .ClearDiscountCouponGroupItem();

        }
    }
}
