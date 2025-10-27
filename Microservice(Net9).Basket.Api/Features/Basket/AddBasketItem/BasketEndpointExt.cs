using Asp.Versioning.Builder;

namespace Microservice_Net9_.Basket.Api.Features.Basket.AddBasketItem
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketItemGroupItem();

        }
    }
}
