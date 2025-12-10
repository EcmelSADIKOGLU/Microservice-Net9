using Microservice_Net9_.Web.Pages.Instructor.ViewModels;
using Microservice_Net9_.Web.Services.Refit;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Text.Json;

namespace Microservice_Net9_.Web.Services
{
    public class CatalogService(ICatalogRefitService catalogRefitService, ILogger<CatalogService> logger)
    {
        public async Task<ServiceResult<List<CategoryViewModel>>> GetCategoriesAsync()
        {
            var response = await catalogRefitService.GetCategoriesAsync();

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content);
                logger.LogError("Error occured while fetching categories");

                return ServiceResult<List<CategoryViewModel>>.Error("Fail to retrieve categories. Please try again later.");
            }

            var categories = response.Content!.Categories.Select(c => new CategoryViewModel(c.Id, c.Name)).ToList();

            return  ServiceResult<List<CategoryViewModel>>.Success(categories);
        }

        public async Task<ServiceResult> CreateCourseAsync(CreateCourseViewModel model)
        {
            StreamPart? pictureStreamPart = null;

            if (model.PictureFormFile is not null && model.PictureFormFile.Length > 0)
            {
                await using var stream = model.PictureFormFile.OpenReadStream();

                var streamPart = new StreamPart(stream, model.PictureFormFile.FileName, model.PictureFormFile.ContentType);
            }
            

            var response = await catalogRefitService.CreateCourseAsync(model.Name, model.Description, model.Price, pictureStreamPart, model.CategoryId.ToString()!);
        }

    }
}
