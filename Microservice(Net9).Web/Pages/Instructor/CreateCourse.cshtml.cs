using Microservice_Net9_.Web.PageModels;
using Microservice_Net9_.Web.Pages.Instructor.ViewModels;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages.Instructor
{
    [Authorize(Roles = "instructor")]
    public class CreateCourseModel(CatalogService catalogService, ILogger<IndexModel> logger) : BasePageModel
    {
        [BindProperty] public CreateCourseViewModel ViewModel { get; set; } = CreateCourseViewModel.Empty;
        public async Task<IActionResult> OnGet()
        {
            var categoriesResult = await catalogService.GetCategoriesAsync();

            if (categoriesResult.isFail) return ErrorPage(categoriesResult);

            ViewModel.SetCategoryDropdownList(categoriesResult.Data!);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await catalogService.CreateCourseAsync(ViewModel);

            if (result.isFail) return ErrorPage(result);

            return RedirectToPage("Courses");
        }
    }
}
