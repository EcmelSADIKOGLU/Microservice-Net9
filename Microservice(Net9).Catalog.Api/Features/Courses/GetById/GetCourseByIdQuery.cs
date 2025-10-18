using Microservice_Net9_.Catalog.Api.Features.Courses.Dtos;

namespace Microservice_Net9_.Catalog.Api.Features.Courses.GetById
{
    public record GetCourseByIdQuery(Guid id) : IRequestByServiceResult<CourseDto>;
}
