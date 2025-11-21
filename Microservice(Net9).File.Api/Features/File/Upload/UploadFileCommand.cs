using Microservice_Net9_.Shared;

namespace Microservice_Net9_.File.Api.Features.File.Upload
{
    public record UploadFileCommand(IFormFile File): IRequestByServiceResult<UploadFileCommandResponse>;
}
