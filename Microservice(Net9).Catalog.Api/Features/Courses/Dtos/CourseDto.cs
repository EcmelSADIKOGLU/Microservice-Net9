using Microservice_Net9_.Catalog.Api.Features.Categories;
using Microservice_Net9_.Catalog.Api.Features.Categories.Dtos;

namespace Microservice_Net9_.Catalog.Api.Features.Courses.Dtos
{
    public record CourseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public string? ImageUrl { get; init; }
        public CategoryDto Category { get; init; }
        public FeatureDto Feature { get; init; }
    }

}
