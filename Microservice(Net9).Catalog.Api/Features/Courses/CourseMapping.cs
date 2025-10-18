using Microservice_Net9_.Catalog.Api.Features.Courses.Create;
using Microservice_Net9_.Catalog.Api.Features.Courses.Dtos;

namespace Microservice_Net9_.Catalog.Api.Features.Courses
{
    public class CourseMapping:Profile
    {
        public CourseMapping()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
            CreateMap<Course, CreateCourseCommand>().ReverseMap();
            
        }
    }
}
