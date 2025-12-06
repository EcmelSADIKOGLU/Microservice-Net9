using System.ComponentModel.DataAnnotations;

namespace Microservice_Net9_.Web.Pages.Auth.SignUp
{
    public record SignUpViewModel
    {
        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; init; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; init; }

        [Display(Name = "Username:")]
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; init; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }

        [Display(Name = "Password Confirm:")]
        [Required(ErrorMessage = "Password Confirm is required")]
        [Compare(nameof(Password), ErrorMessage = "The Password dont match" )]
        public string? PasswordConfirm { get; init; }

        public static SignUpViewModel Empty => new()
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            UserName = string.Empty,
            Email = string.Empty,
            Password = string.Empty,
            PasswordConfirm = string.Empty
        };

        public static SignUpViewModel GetExampleModel => new()
        {
            FirstName = "Ali",
            LastName = "KAYA",
            UserName = "AliKY",
            Email = "ali@gmail.com",
            Password = "Ali123*",
            PasswordConfirm = "Ali123*"
        };
    }

}
