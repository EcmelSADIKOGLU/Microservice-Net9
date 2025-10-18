
using Microservice_Net9_.Catalog.Api.Repositories;

namespace Microservice_Net9_.Catalog.Api.Features.Courses.Delete
{
    public class DeleteCourseCommandHandler (AppDbContext context) : IRequestHandler<DeleteCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FindAsync(request.id, cancellationToken);

            if(course is null)
            {
               return ServiceResult.Error("Course does not exist", $"There is not a course with id {request.id}", HttpStatusCode.NotFound); 

            }

            context.Courses.Remove(course);
            await context.SaveChangesAsync();

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
