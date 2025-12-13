namespace Microservice_Net9_.Web.Pages.Instructor.Dtos
{
    public record CourseDto
    {
        public Guid Id { get; init; }
 //       public DateTime CreateTime { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public string? ImageUrl { get; init; }
        public CategoryDto Category { get; init; }
        public FeatureDto Feature { get; init; }
    }

}
