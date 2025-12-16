using Microservice_Net9_.Web.Pages.Order.Dtos;
using Microservice_Net9_.Web.Pages.Order.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages.Order
{
    public class HistoryModel : PageModel
    {
        public List<OrderHistoryViewModel> OrderHistoryList { get; set; }
        public void OnGet()
        {
        }
    }
}
