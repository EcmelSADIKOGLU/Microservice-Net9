
using System.ComponentModel.DataAnnotations;

namespace Microservice_Net9_.Web.Pages.Auth.SignIn
{
    public record SignInViewModel
    {
        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; init; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }

        public static SignInViewModel Empty => new()
        {
            Email = string.Empty,
            Password = string.Empty,
        };

        public static SignInViewModel GetExampleModel => new()
        {
            Email = "ali@gmail.com",
            Password = "Ali123*",
        };
    }
}
