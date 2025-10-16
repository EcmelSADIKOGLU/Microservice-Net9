using AutoMapper;
using Microservice_Net9_.Catalog.Api.Features.Categories.Dtos;

namespace Microservice_Net9_.Catalog.Api.Features.Categories
{
    public class CategoryMapping: Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();  
        }
    }
}
