using Microservice_Net9_.Bus.Events;
using Microservice_Net9_.Catalog.Api.Repositories;

namespace Microservice_Net9_.Catalog.Api.Consumers
{
    public class CoursePictureUploadedEventConsumer(AppDbContext dbContext) : IConsumer<CoursePictureUploadedEvent>
    {
        public async Task Consume(ConsumeContext<CoursePictureUploadedEvent> context)
        {
            var course = await dbContext.Courses.FindAsync(context.Message.CourseId);

            if (course is null)
            {
               throw new Exception("Course not found");
            }

            course.ImageUrl = context.Message.ImageUrl;

            await dbContext.SaveChangesAsync();


        }
    }
}
