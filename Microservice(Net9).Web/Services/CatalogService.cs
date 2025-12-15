using Microservice_Net9_.Web.Pages.Instructor.ViewModels;
using Microservice_Net9_.Web.Services.Refit;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Text.Json;

namespace Microservice_Net9_.Web.Services
{
    public class CatalogService(ICatalogRefitService catalogRefitService,
        UserService userService,
        ILogger<CatalogService> logger)
    {
        public async Task<ServiceResult<List<CategoryViewModel>>> GetCategoriesAsync()
        {
            var response = await catalogRefitService.GetCategoriesAsync();

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(response.Error.Content);
                logger.LogError("Error occured while fetching categories");

                return ServiceResult<List<CategoryViewModel>>.Error("Fail to retrieve categories. Please try again later.");
            }

            var categories = response.Content!.Categories.Select(c => new CategoryViewModel(c.Id, c.Name)).ToList();

            return  ServiceResult<List<CategoryViewModel>>.Success(categories);
        }

        public async Task<ServiceResult> CreateCourseAsync(CreateCourseViewModel model)
        {
            StreamPart? pictureStreamPart = null;
            await using var stream = model.PictureFormFile?.OpenReadStream();

            if (model.PictureFormFile is not null && model.PictureFormFile.Length > 0)
            {
                pictureStreamPart = new StreamPart(stream!, model.PictureFormFile.FileName, model.PictureFormFile.ContentType);
            }

            var response = await catalogRefitService.CreateCourseAsync(
                model.Name, 
                model.Description, 
                model.Price, 
                pictureStreamPart, 
                model.CategoryId.ToString()!);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occured while fetching categories");

                return ServiceResult<List<CategoryViewModel>>.Error("Fail to retrieve categories. Please try again later.");
            }

            return ServiceResult.Success();
        }

        public async Task<ServiceResult<CourseListViewModel>> GetCoursesByUserIdAsync()
        {
            var userId = userService.UserId;
            var response = await catalogRefitService.GetCoursesByUserIdAsync(userId);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(response.Error.Content);
                logger.LogError("Error occured while fetching courses by userId");

                return ServiceResult<CourseListViewModel>.Error("Fail to retrieve categories. Please try again later.");
            }

            var courses = response.Content!.Courses;
            var courseModels = courses.Select(c => new CourseViewModel(
                c.Id,
                c.Name,
                c.Description,
                c.Price,
                c.ImageUrl,
                c.CreateTime.ToString("d"),
                c.Feature.EducatorFullName,
                c.Category.Name,
                c.Feature.Duration,
                c.Feature.Rate
                )).ToList();

            return ServiceResult<CourseListViewModel>.Success( new CourseListViewModel(courseModels));
        }

        public async Task<ServiceResult<CourseListViewModel>> GetCoursesAsync()
        {
            var response = await catalogRefitService.GetCoursesAsync();

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(response.Error!.Content!);
                logger.LogError("Error occured while fetching courses by userId");

                return ServiceResult<CourseListViewModel>.Error("Fail to retrieve categories. Please try again later.");
            }

            var courses = response.Content!.Courses;
            var courseModels = courses.Select(c => new CourseViewModel(
                c.Id,
                c.Name,
                c.Description,
                c.Price,
                c.ImageUrl,
                c.CreateTime.ToString("d"),
                c.Feature.EducatorFullName,
                c.Category.Name,
                c.Feature.Duration,
                c.Feature.Rate
                )).ToList();

            return ServiceResult<CourseListViewModel>.Success(new CourseListViewModel(courseModels));
        }

        public async Task<ServiceResult> DeleteCourseAsync(Guid courseId)
        {
            var response = await catalogRefitService.DeleteCourseAsync(courseId);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(response.Error.Content);
                logger.LogError("Error occured while deleting course");

                return ServiceResult.Error("Fail to delete course. Please try again later.");
            }
           return ServiceResult.Success();
        }
    }
}
