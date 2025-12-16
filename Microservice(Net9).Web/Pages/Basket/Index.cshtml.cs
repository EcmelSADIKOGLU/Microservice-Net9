using Microservice_Net9_.Web.PageModels;
using Microservice_Net9_.Web.Pages.Basket.Dtos;
using Microservice_Net9_.Web.Pages.Basket.ViewModels;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages.Basket
{
    public class IndexModel(BasketService basketService, CatalogService catalogService) : BasePageModel
    {
        public BasketPageViewModel Basket { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var response = await basketService.GetBasketAsync();

            if (response.isFail) return ErrorPage(response, "Index");

            Basket = response.Data!;
            return Page();
        }

        public async Task<IActionResult> OnGetAddItemToBasketAsync(Guid id)
        {
            var courseResponse = await catalogService.GetCourseByIdAsync(id);
            
            if (courseResponse.isFail) return ErrorPage(courseResponse, "Index");

            var courseDto = courseResponse.Data!;

            var createOrUpdateBasket = new AddBasketItemRequest(courseDto.Id, courseDto.Name,
                courseDto.Price, courseDto.ImageUrl, courseDto.EducatorFullName);

            var response = await basketService.AddBasketItemAsync(createOrUpdateBasket);

            return response.isFail ? ErrorPage(response, "Index") : SuccessPage("course added to basket", "Index");
        }
    }
}
