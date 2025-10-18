using Microservice_Net9_.Catalog.Api.Features.Courses.Dtos;
using Microservice_Net9_.Catalog.Api.Features.Courses.GetAll;
using Microservice_Net9_.Catalog.Api.Repositories;

namespace Microservice_Net9_.Catalog.Api.Features.Courses.GetAllByUserId
{
    public class GetAllCoursesByUserIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCoursesByUserIdQuery, ServiceResult<GetAllCoursesResponse>>
    {
        public async Task<ServiceResult<GetAllCoursesResponse>> Handle(GetAllCoursesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = request.id;

            
            if (false)// Check is user exist
            {
                return ServiceResult<GetAllCoursesResponse>.Error("User could not found", $"There is no user with id {userId}", HttpStatusCode.NotFound);
            }   

            var courses = await context.Courses.Where(x => x.UserId == userId).ToListAsync(cancellationToken);

            var categories = await context.Categories.ToListAsync(cancellationToken);

            foreach (var course in courses)
            {
                course.Category = categories.First(x => x.Id == course.CategoryId);

            }
            var list = mapper.Map<List<CourseDto>>(courses);

            var response = new GetAllCoursesResponse(list);
            return ServiceResult<GetAllCoursesResponse>.SuccessAsOk(response);
        }
    }
}
