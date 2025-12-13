using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages.Instructor
{
    public class CoursesModel(CatalogService catalogService) : PageModel
    {
        public void OnGet()
        {
        }
    }
}
