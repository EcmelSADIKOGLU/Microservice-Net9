using Microservice_Net9_.Web.Pages.Instructor.Dtos;
using Microservice_Net9_.Web.Pages.Instructor.ViewModels;
using Refit;

namespace Microservice_Net9_.Web.Services.Refit
{
    public interface ICatalogRefitService
    {
        [Get("/v1/catalog/categories")]
        Task<ApiResponse<ServiceResult<List<CategoryDto>>>> GetCategoriesAsync();

        [Post("/v1/catalog/courses")]
        Task<ApiResponse<ServiceResult>> CreateCourseAsync(CreateCourseRequest createCourseRequest);

        [Put("/v1/catalog/courses")]
        Task<ApiResponse<ServiceResult>> UpgradeCourseAsync(UpdateCourseRequest createCourseRequest);

        [Delete("/v1/catalog/courses/{id}")]
        Task<ApiResponse<ServiceResult>> DeleteCourseAsync(Guid id);
    }
}
