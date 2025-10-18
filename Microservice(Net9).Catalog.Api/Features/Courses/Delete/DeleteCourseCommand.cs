namespace Microservice_Net9_.Catalog.Api.Features.Courses.Delete
{
    public record DeleteCourseCommand(Guid id) : IRequestByServiceResult;
}
