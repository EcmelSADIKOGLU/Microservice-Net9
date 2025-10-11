namespace Microservice_Net9_.Catalog.Api.Features.Courses
{
    //Base entity değil. Id yok.
    public class Feature
    {
        //public Guid CourseId { get; set; } = default!;     neden yazmadık?
        public int Duration { get; set; }
        public float Rate { get; set; }
        public string EducatorFullName { get; set; } = default!;
    }
}
