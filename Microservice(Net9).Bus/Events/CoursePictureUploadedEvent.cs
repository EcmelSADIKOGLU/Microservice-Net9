using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice_Net9_.Bus.Events
{
    public record CoursePictureUploadedEvent(Guid CourseId, string ImageUrl);
}
