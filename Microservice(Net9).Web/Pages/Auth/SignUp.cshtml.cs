using Microservice_Net9_.Web.Pages.Auth.SignUp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages.Auth
{
    public class SignUpModel : PageModel
    {
        [BindProperty] public required SignUpViewModel SignUpViewModel { get; set; } = SignUpViewModel.Empty;
        public void OnGet()
        {
        }
    }
}
