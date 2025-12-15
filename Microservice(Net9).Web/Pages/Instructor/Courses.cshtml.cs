using Microservice_Net9_.Web.PageModels;
using Microservice_Net9_.Web.Pages.Instructor.ViewModels;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages.Instructor
{
    [Authorize(Roles = "instructor")]
    public class CoursesModel(CatalogService catalogService, ILogger<IndexModel> logger) : BasePageModel
    {
        public CourseListViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await catalogService.GetCoursesByUserIdAsync();

            if (result.isFail) return ErrorPage(result);

            ViewModel = result.Data!;
            return Page();
        }

        public async Task<IActionResult> OnGetDeleteAsync(Guid id)
        {
            var result = await catalogService.DeleteCourseAsync(id);

            if (result.isFail) return ErrorPage(result);

            return RedirectToPage();
        }
    }
}
