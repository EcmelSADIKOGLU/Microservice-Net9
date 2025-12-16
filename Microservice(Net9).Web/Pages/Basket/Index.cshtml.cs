using Microservice_Net9_.Basket.Api.Features.Basket.ApplyDiscountCoupon;
using Microservice_Net9_.Web.PageModels;
using Microservice_Net9_.Web.Pages.Basket.Dtos;
using Microservice_Net9_.Web.Pages.Basket.ViewModels;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages.Basket
{
    public class IndexModel(BasketService basketService, CatalogService catalogService, DiscountService discountService) : BasePageModel
    {
        public BasketPageViewModel Basket { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var response = await basketService.GetBasketAsync();

            if (response.isFail) return ErrorPage(response, "Index");

            Basket = response.Data!;
            return Page();
        }

        public async Task<IActionResult> OnGetAddItemToBasketAsync(Guid courseId)
        {
            var courseResponse = await catalogService.GetCourseByIdAsync(courseId);
            
            if (courseResponse.isFail) return ErrorPage(courseResponse, "Index");

            var courseDto = courseResponse.Data!;

            var createOrUpdateBasket = new AddBasketItemRequest(courseDto.Id, courseDto.Name,
                courseDto.Price, courseDto.ImageUrl, courseDto.EducatorFullName);

            var response = await basketService.AddBasketItemAsync(createOrUpdateBasket);

            return response.isFail ? ErrorPage(response, "Index") : SuccessPage("course added to basket", "Index");
        }

        public async Task<IActionResult> OnGetDeleteItemFromBasketAsync(Guid courseId)
        {
            var response = await basketService.DeleteBasketItemAsync(courseId);

            return response.isFail ? ErrorPage(response, "Index") : SuccessPage("course deleted from basket", "Index");
        }

        public async Task<IActionResult> OnPostApplyDiscountAsync(string CouponCode)
        {
            var discountResponse = await discountService.GetDiscountByCodeAsync(CouponCode);

            if (discountResponse.isFail) return ErrorPage(discountResponse, "Index");

            var response = await basketService.ApplyDiscountCouponAsync(discountResponse.Data!);

            return response.isFail ? ErrorPage(response, "Index") : SuccessPage("discount applied to basket", "Index");
        }

        public async Task<IActionResult> OnGetRemoveDiscountAsync()
        {
            var response = await basketService.DeleteDiscountCouponAsync();
            return response.isFail ? ErrorPage(response, "Index") : SuccessPage("discount removed from basket", "Index");
        }
    }
}
