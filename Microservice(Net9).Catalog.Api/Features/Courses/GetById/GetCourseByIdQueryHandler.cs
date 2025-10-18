using Microservice_Net9_.Catalog.Api.Features.Courses.Dtos;
using Microservice_Net9_.Catalog.Api.Repositories;

namespace Microservice_Net9_.Catalog.Api.Features.Courses.GetById
{
    public class GetCourseByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto>>
    {
        public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {

            var course = await context.Courses.FindAsync(request.id, cancellationToken);

            if (course is null)
            {
                return ServiceResult<CourseDto>.Error("Course could not found", $" There is not a course with id{request.id}", HttpStatusCode.NotFound);
            }

            var category = (await context.Categories.FindAsync(course.CategoryId, cancellationToken))!;  
            //Kesinlikle category var. Null gelemez.

            course.Category = category;

            var courseDto = mapper.Map<CourseDto>(course);  

            return ServiceResult<CourseDto>.SuccessAsOk(courseDto);

        }
    }
}
