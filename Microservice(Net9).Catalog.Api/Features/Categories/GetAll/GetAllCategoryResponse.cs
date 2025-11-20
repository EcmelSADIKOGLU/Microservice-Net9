using Microservice_Net9_.Catalog.Api.Features.Categories.Dtos;

namespace Microservice_Net9_.Catalog.Api.Features.Categories.GetAll
{
    public record GetAllCategoryResponse(List<CategoryDto> Categories);
}
