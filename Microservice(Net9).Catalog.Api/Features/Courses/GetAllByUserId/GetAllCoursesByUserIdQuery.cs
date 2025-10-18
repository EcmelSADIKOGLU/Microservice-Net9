using Microservice_Net9_.Catalog.Api.Features.Courses.GetAll;

namespace Microservice_Net9_.Catalog.Api.Features.Courses.GetAllByUserId
{
    public record GetAllCoursesByUserIdQuery(Guid id) : IRequestByServiceResult<GetAllCoursesResponse>;
}
