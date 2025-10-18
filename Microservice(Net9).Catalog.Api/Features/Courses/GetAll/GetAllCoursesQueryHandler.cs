
using Microservice_Net9_.Catalog.Api.Features.Courses.Dtos;
using Microservice_Net9_.Catalog.Api.Repositories;

namespace Microservice_Net9_.Catalog.Api.Features.Courses.GetAll
{
    public class GetAllCoursesQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCoursesQuery, ServiceResult<GetAllCoursesResponse>>
    {
        public async Task<ServiceResult<GetAllCoursesResponse>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses.ToListAsync(cancellationToken);

            var categories = await context.Categories.ToListAsync(cancellationToken);

            foreach (var course in courses)
            {
                course.Category = categories.First(x=>x.Id == course.CategoryId);

            }
            var list = mapper.Map<List<CourseDto>>(courses);

            var response = new GetAllCoursesResponse(list);
            return ServiceResult<GetAllCoursesResponse>.SuccessAsOk(response);
        }
    }
}
