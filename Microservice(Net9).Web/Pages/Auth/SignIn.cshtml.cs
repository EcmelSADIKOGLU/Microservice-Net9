using Microservice_Net9_.Web.Pages.Auth.SignIn;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microservice_Net9_.Web.Pages.Auth
{
    public class SignInModel(SignInService signInService) : PageModel
    {
        [BindProperty] public required SignInViewModel SignInViewModel { get; set; } = SignInViewModel.GetExampleModel;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }

            ServiceResult tokenResponse = await signInService.AuthenticateAsync(SignInViewModel);

            if (tokenResponse.isFail)
            {
                ModelState.AddModelError(string.Empty, tokenResponse.Fail!.Title!);

                if (!string.IsNullOrEmpty(tokenResponse.Fail.Detail))
                {
                    ModelState.AddModelError(string.Empty, tokenResponse.Fail.Detail);
                }
                return Page();
            }

            return RedirectToPage("/Index");
        }

        //SignIn/SignOut ismi ile erişilebilir.
        public async Task<IActionResult> OnGetSignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}
