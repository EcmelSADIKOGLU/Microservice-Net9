using MassTransit;
using Microservice_Net9_.Bus.Commands;
using Microservice_Net9_.Bus.Events;
using Microsoft.Extensions.FileProviders;

namespace Microservice_Net9_.File.Api.Consumers
{
    public class UploadCoursePictureCommandConsumer(IFileProvider fileProvider, IPublishEndpoint publishEndpoint)
    : IConsumer<UploadCoursePictureCommand>
    {
        public async Task Consume(ConsumeContext<UploadCoursePictureCommand> context)
        {

            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(context.Message.FileName)}"; // .jpg

            var uploadPath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath!, newFileName);


            await System.IO.File.WriteAllBytesAsync(uploadPath, context.Message.Picture);

            await publishEndpoint.Publish(new CoursePictureUploadedEvent(context.Message.CourseId,
                $"files/{newFileName}"));
        }
    }
}
