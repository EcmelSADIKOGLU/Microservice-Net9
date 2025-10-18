using Microservice_Net9_.Catalog.Api.Features.Courses.Dtos;

namespace Microservice_Net9_.Catalog.Api.Features.Courses.GetAll
{
    public record GetAllCoursesResponse(List<CourseDto> Courses);
}
