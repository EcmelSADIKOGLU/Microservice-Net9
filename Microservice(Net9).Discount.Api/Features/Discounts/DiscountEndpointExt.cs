
using Asp.Versioning.Builder;
using Microservice_Net9_.Discount.Api.Features.Discounts.CreateDiscount;
using Microservice_Net9_.Discount.Api.Features.Discounts.GetByCode;

namespace Microservice_Net9_.Discount.Api.Features.Discounts
{
    public static class DiscountEndpointExt
    {
        public static void AddDiscountEndpointGroupExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/discounts").WithTags("Discounts")
                .WithApiVersionSet(apiVersionSet)
                .CreateDiscountGroupItem()
                .GetDiscountByCodeEndpointGroupItem();



            //app.MapGroup("api/v{version:apiVersion}/categories").WithTags("Categories");
               
        }

    }
}
