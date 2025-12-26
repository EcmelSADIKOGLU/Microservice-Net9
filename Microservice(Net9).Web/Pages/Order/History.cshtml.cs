using Microservice_Net9_.Web.PageModels;
using Microservice_Net9_.Web.Pages.Order.Dtos;
using Microservice_Net9_.Web.Pages.Order.ViewModels;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages.Order
{
    public class HistoryModel(OrderService orderService) : BasePageModel
    {
        public List<OrderHistoryViewModel> OrderHistoryList { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var response = await orderService.GetOrderHistoryAsync();

            if (response.isFail)
            {
                return ErrorPage(response);
            }

            OrderHistoryList = response.Data!;

            return Page();
        }
    }
}
