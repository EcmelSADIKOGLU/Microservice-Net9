
using Microservice_Net9_.Catalog.Api.Repositories;

namespace Microservice_Net9_.Catalog.Api.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {

            var course = mapper.Map<Course>(request);
            var isCourseExist = await context.Courses.AnyAsync(x=> x.Name == course.Name ,cancellationToken);

            if (isCourseExist)
            {
                return ServiceResult<Guid>.Error("Course could not created.", $"Already there is a course {request.Name} named.", HttpStatusCode.BadRequest);
            }
            
            var isCategoryExist = await context.Categories.AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

            if (!isCategoryExist)
            {
                return ServiceResult<Guid>.Error("Course could not created.", $"There is no category with id {request.CategoryId}.", HttpStatusCode.NotFound);
            }

            course.Id = NewId.NextSequentialGuid();
            course.UserId = NewId.NextSequentialGuid();  //FromToken
            course.CreateTime = DateTime.Now;

            course.Feature = new Feature
            {
                Duration = 0, // Calculate by video
                Rate = 0,
                EducatorFullName = "Default Educator" //FromToken
            };
            

            await context.Courses.AddAsync(course, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.SuccessAsCreated(course.Id, $"/api/courses/{course.Id}");
           
        }
    }
}
