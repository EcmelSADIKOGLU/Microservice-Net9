using Microservice_Net9_.Web.PageModels;
using Microservice_Net9_.Web.Pages.Instructor.ViewModels;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages.Courses
{
    public class DetailModel(CatalogService catalogService) : BasePageModel
    {
        public CourseViewModel CourseViewModel { get; set; } = CourseViewModel.Empty;
        public async Task<IActionResult> OnGetAsync(Guid courseId)
        {
            var response = await catalogService.GetCourseByIdAsync(courseId);
            if (response.isFail)
            {
                return ErrorPage(response);
            }
            CourseViewModel = response.Data!;
            return Page();
        }
    }
}
