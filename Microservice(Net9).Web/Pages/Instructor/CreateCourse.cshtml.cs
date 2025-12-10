using Microservice_Net9_.Web.Pages.Instructor.ViewModels;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Microservice_Net9_.Web.Pages.Instructor
{
    public class CreateCourseModel(CatalogService catalogService) : PageModel
    {
        public CreateCourseViewModel ViewModel { get; set; } = CreateCourseViewModel.Empty;
        public async Task OnGet()
        {
            var categoriesResult = await catalogService.GetCategoriesAsync();
            if (categoriesResult.isFail)
            {
                //TODO: redirect
                //ModelState.AddModelError(null, categoriesResult.Fail.Title);
            }

            ViewModel.SetCategoryDropdownList(categoriesResult.Data!);


        }
    }
}
