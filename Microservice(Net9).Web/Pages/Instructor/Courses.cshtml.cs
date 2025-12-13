using Microservice_Net9_.Web.Pages.Instructor.ViewModels;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages.Instructor
{
    public class CoursesModel(CatalogService catalogService) : PageModel
    {
        public CourseListViewModel ViewModel { get; set; }

        public async Task OnGetAsync()
        {
            var result = await catalogService.GetCoursesByUserIdAsync();
            if (result.isFail)
            {
                //TODO: redirect
                //ModelState.AddModelError(null, categoriesResult.Fail.Title);

            }
            ViewModel = result.Data!;
        }

        public async Task<IActionResult> OnGetDeleteAsync(Guid id)
        {
            var result = await catalogService.DeleteCourseAsync(id);
            if (result.isFail)
            {
                //TODO: redirect
                //ModelState.AddModelError(null, categoriesResult.Fail.Title);

            }
            return RedirectToPage();
        }
    }
}
