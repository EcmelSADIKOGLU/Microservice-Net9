using MediatR;
using Microservice_Net9_.Shared;
using Microsoft.Extensions.FileProviders;
using System.Net;

namespace Microservice_Net9_.File.Api.Features.File.Delete
{
    public class DeleteFileCommandHandler(IFileProvider fileProvider) : IRequestHandler<DeleteFileCommand, ServiceResult>
    {
        public Task<ServiceResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var fileInfo = fileProvider.GetFileInfo(Path.Combine("files", request.FileName));

            if (!fileInfo.Exists)
            {
                return Task.FromResult(ServiceResult.Error("File not found", HttpStatusCode.NotFound));
            }

            System.IO.File.Delete(fileInfo.PhysicalPath!);

            return Task.FromResult(ServiceResult.SuccessAsNoContent());  

        }
    }
}
