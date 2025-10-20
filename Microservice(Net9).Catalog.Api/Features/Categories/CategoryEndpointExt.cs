using Asp.Versioning.Builder;
using Microservice_Net9_.Catalog.Api.Features.Categories.Create;
using Microservice_Net9_.Catalog.Api.Features.Categories.GetAll;
using Microservice_Net9_.Catalog.Api.Features.Categories.GetCategoryById;

namespace Microservice_Net9_.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/categories").WithTags("Categories")
                .WithApiVersionSet(apiVersionSet)
                .CreateCategoryGroupItem()
                .GetAllCategoryGroupItem()
                .GetCategoryByIdGroupItem();

        }
    }
}
