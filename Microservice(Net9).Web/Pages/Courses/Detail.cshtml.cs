using Microservice_Net9_.Web.PageModels;
using Microservice_Net9_.Web.Pages.Instructor.ViewModels;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages.Courses
{
    public class DetailModel(CatalogService catalogService) : BasePageModel
    {
        public CourseViewModel CourseViewModel { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var response = await catalogService.GetCourseByIdAsync(id);
            if (response.isFail)
            {
                return ErrorPage(response);
            }
            CourseViewModel = response.Data!;
            return Page();
        }
    }
}
