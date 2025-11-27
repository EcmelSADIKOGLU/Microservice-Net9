using Asp.Versioning.Builder;
using Microservice_Net9_.File.Api.Features.File.Delete;
using Microservice_Net9_.File.Api.Features.File.Upload;


namespace Microservice_Net9_.File.Api.Features.File
{
    public static class FileEndpointExt
    {
        public static void AddFileEndpointGroupExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/files").WithTags("Files")
                .WithApiVersionSet(apiVersionSet)
                .UploadFileGroupItem()
                .DeleteFileGroupItem()
                .RequireAuthorization();

               
        }

    }
}
