namespace Microservice_Net9_.Catalog.Api.Features.Courses.Update
{
    public record UpdateCourseCommand(
        Guid id,
        string Name,
        string Description,
        decimal Price,
        string? ImageUrl,
        Guid CategoryId):IRequestByServiceResult;
}
