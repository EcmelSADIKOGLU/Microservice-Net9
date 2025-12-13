using Microservice_Net9_.Web.Pages.Instructor.ViewModels;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages.Instructor
{
    public class CoursesModel(CatalogService catalogService) : PageModel
    {
        public CourseListViewModel Model { get; set; }

        public async void OnGetAsync()
        {
            var result = await catalogService.GetCoursesByUserIdAsync();
            if (result.isFail)
            {
                //TODO: redirect
                //ModelState.AddModelError(null, categoriesResult.Fail.Title);

            }
            Model = result.Data!;
        }
    }
}
