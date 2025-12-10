using Microservice_Net9_.Web.Pages.Instructor.ViewModels;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages.Instructor
{
    [Authorize(Roles = "instructor")]
    public class CreateCourseModel(CatalogService catalogService) : PageModel
    {
        [BindProperty] public CreateCourseViewModel ViewModel { get; set; } = CreateCourseViewModel.Empty;
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

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await catalogService.CreateCourseAsync(ViewModel);

            if (result.isFail)
            {
                //TODO: redirect
            }

            return RedirectToPage("Course");
        }
    }
}
