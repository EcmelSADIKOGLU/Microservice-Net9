using Microservice_Net9_.Web.PageModels;
using Microservice_Net9_.Web.Pages.Instructor.ViewModels;
using Microservice_Net9_.Web.Services;
using Microservice_Net9_.Web.Services.Refit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages
{
    public class IndexModel(CatalogService catalogService, ILogger<IndexModel> logger) : BasePageModel
    {
        public CourseListViewModel CourseListViewModel { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var coursesAsResult = await catalogService.GetCoursesAsync();

            if (coursesAsResult.isFail) return ErrorPage(coursesAsResult);

            CourseListViewModel = coursesAsResult.Data!;

            return Page();
        }


        
    }
}
