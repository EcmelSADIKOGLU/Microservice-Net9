using Microservice_Net9_.Web.Pages.Instructor.Dtos;

namespace Microservice_Net9_.Web.Services.ResponseDtos
{
    public record GetAllCategoryResponseDto(List<CategoryDto> Categories);
}
