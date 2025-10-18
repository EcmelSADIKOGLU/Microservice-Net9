using Microservice_Net9_.Catalog.Api.Features.Courses.Create;
using Microservice_Net9_.Catalog.Api.Features.Courses.GetAll;
using Microservice_Net9_.Catalog.Api.Features.Courses.GetById;
using Microservice_Net9_.Catalog.Api.Features.Courses.Update;
using Microservice_Net9_.Catalog.Api.Features.Courses.Delete;
using Microservice_Net9_.Catalog.Api.Features.Courses.GetAllByUserId;

namespace Microservice_Net9_.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/courses").WithTags("Courses")
                .CreateCourseGroupItem()
                .GetAllCoursesGroupItem()
                .GetCourseByIdGroupItem()
                .UpdateCourseGroupItem()
                .DeleteCourseGroupItem()
                .GetAllCoursesByUserIdGroupItem();

        }
    }
}
