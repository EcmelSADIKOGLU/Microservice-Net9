using Microservice_Net9_.Web.Pages.Instructor.ViewModels;
using Microservice_Net9_.Web.Services.Refit;
using Microsoft.AspNetCore.Mvc;
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

            var categories = response.Content!.Data!.Select(c => new CategoryViewModel(c.Id, c.Name)).ToList();

            return  ServiceResult<List<CategoryViewModel>>.Success(categories);
        }

    }
}
