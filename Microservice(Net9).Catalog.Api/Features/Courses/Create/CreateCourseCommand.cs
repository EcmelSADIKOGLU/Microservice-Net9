namespace Microservice_Net9_.Catalog.Api.Features.Courses.Create
{
    public record CreateCourseCommand() : IRequestByServiceResult<Guid>
    {
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public IFormFile? Picture { get; set; }
        public decimal Price { get; init; }
        public Guid CategoryId { get; init; }

    } 

}
