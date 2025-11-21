using Microservice_Net9_.Shared;

namespace Microservice_Net9_.File.Api.Features.File.Delete
{
    public record DeleteFileCommand(string FileName): IRequestByServiceResult;
}
