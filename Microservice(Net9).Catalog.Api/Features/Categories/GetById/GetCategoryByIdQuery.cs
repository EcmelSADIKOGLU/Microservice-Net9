using Microservice_Net9_.Catalog.Api.Features.Categories.Dtos;

namespace Microservice_Net9_.Catalog.Api.Features.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid id) : IRequestByServiceResult<CategoryDto>;
}
