using Microservice_Net9_.Web.PageModels;
using Microservice_Net9_.Web.Pages.Basket.ViewModels;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages.Basket
{
    public class IndexModel(BasketService basketservice) : BasePageModel
    {
        public BasketPageViewModel Basket { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var response = await basketservice.GetBasketAsync();

            if (response.isFail) return ErrorPage(response, "Index");

            Basket = response.Data!;
            return Page();
        }

        public async Task<IActionResult> OnGetAddItemToBasketAsync(Guid id)
        {
            //TODO: Need GetCourse();
            //if (coursesAsResult.isFail) return ErrorPage(coursesAsResult);

            //CourseListViewModel = coursesAsResult.Data!;

            return Page();
        }
    }
}
