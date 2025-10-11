using Microservice_Net9_.Catalog.Api.Features.Courses;
using Microservice_Net9_.Catalog.Api.Repositories;

namespace Microservice_Net9_.Catalog.Api.Features.Categories
{
    public class Category:BaseEntity
    {
        //sonuna bu şekilde default yazmak, mutlaka doldurulması gerektiği anlamına geliyor
        public string Name { get; set; } = default!;
        public List<Course>? Courses { get; set; }
    }
}
