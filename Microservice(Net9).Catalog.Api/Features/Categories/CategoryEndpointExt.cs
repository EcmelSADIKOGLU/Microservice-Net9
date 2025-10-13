using Microservice_Net9_.Catalog.Api.Features.Categories.Create;
using Microservice_Net9_.Shared.Filters;

namespace Microservice_Net9_.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories").CreateCategoryGroupItem();
        }
    }
}
