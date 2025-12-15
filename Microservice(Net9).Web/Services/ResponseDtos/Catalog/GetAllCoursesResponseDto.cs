using Microservice_Net9_.Web.Pages.Instructor.Dtos;

namespace Microservice_Net9_.Web.Services.ResponseDtos.Catalog
{
    public record GetAllCoursesResponseDto(List<CourseDto> Courses);
}
