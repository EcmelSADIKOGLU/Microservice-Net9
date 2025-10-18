using Microservice_Net9_.Catalog.Api.Features.Categories;
using Microservice_Net9_.Catalog.Api.Repositories;

namespace Microservice_Net9_.Catalog.Api.Features.Courses
{
    public class Course:BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreateTime { get; set; } // offsetler ile çalışmaya dikkat et
        public Guid UserId { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = default!;

        public Feature Feature { get; set; } = default!;

    }
}
