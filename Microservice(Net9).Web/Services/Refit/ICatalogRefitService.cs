using Microservice_Net9_.Web.Pages.Instructor.Dtos;
using Microservice_Net9_.Web.Pages.Instructor.ViewModels;
using Microservice_Net9_.Web.Services.ResponseDtos;
using Refit;

namespace Microservice_Net9_.Web.Services.Refit
{
    public interface ICatalogRefitService
    {   
        [Get("/api/v1/categories")]
        Task<ApiResponse<GetAllCategoryResponseDto>> GetCategoriesAsync();

        [Get("/api/v1/courses/GetAllCoursesByUserId/{userId}")]
        Task<ApiResponse<GetAllCoursesResponseDto>> GetCoursesByUserIdAsync(Guid userId);

        

        [Multipart]
        [Post("/api/v1/courses")]
        Task<ApiResponse<object>> CreateCourseAsync(
            [AliasAs("Name")] string Name,
            [AliasAs("Description")] string Description,
            [AliasAs("Price")] decimal Price,
            [AliasAs("Picture")] StreamPart? Picture,
            [AliasAs("CategoryId")] string CategoryId);


        [Put("/api/v1/courses")]
        Task<ApiResponse<object>> UpgradeCourseAsync(UpdateCourseRequest createCourseRequest);

        [Delete("/api/v1/courses/{courseId}")]
        Task<ApiResponse<object>> DeleteCourseAsync(Guid courseId);

    }
}
