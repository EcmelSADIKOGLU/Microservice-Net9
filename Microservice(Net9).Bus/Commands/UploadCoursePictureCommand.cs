using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice_Net9_.Bus.Commands
{
    public record UploadCoursePictureCommand(Guid CourseId, Byte[] Picture, string FileName);
}
