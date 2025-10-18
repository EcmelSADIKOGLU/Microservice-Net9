using Microservice_Net9_.Catalog.Api.Repositories;

namespace Microservice_Net9_.Catalog.Api.Features.Courses.Update
{
    public class UpdateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<UpdateCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FindAsync(request.id, cancellationToken);

            if (course is null)
            {
                return ServiceResult.Error("Course could not found", $" There is not a course with id{request.id}", HttpStatusCode.NotFound);
            }

            course.Name = request.Name;
            course.Description = request.Description;
            course.Price = request.Price;
            course.ImageUrl = request.ImageUrl;
            course.CategoryId = request.CategoryId;
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
