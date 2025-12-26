using Microservice_Net9_.Web.PageModels;
using Microservice_Net9_.Web.Pages.Order.ViewModels;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Microservice_Net9_.Web.Pages.Order
{
    public class CreateModel(OrderService orderService, BasketService basketService) : BasePageModel
    {
        [BindProperty] public CreateOrderViewModel CreateOrderViewModel { get; set; } = CreateOrderViewModel.Example;
        public async Task<IActionResult> OnGetAsync()
        {
            var loadResult = await LoadInitialDatas();
            if (loadResult != null) return loadResult;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync() 
        {
            var loadResult = await LoadInitialDatas();
            if (loadResult != null) return loadResult;

            var orderCreateResponse = await orderService.CreateOrderAsync(CreateOrderViewModel);
            if (orderCreateResponse.isFail)
            {
                return ErrorPage(orderCreateResponse);
            }
 
            return SuccessPage("Order created successfully", "Result");
        }

        public async Task<IActionResult?> LoadInitialDatas()
        {
            var basketResponse = await basketService.GetOrderItemViewModels();
            if (basketResponse.isFail)
            {
                return ErrorPage(basketResponse, "/Basket/Index");
            }

            CreateOrderViewModel.OrderItems = basketResponse.Data.Items;
            CreateOrderViewModel.DiscountRate = basketResponse.Data.DiscountRate;

            return null;
        }
    }
}
