using System.ComponentModel.DataAnnotations;

namespace Microservice_Net9_.Web.Pages.Auth.SignUp
{
    public record SignUpViewModel(
        [Display(Name = "First Name:")] string FirstName,
        [Display(Name = "Last Name:")] string LastName,
        [Display(Name = "Username:")] string UserName,
        [Display(Name = "Email:")] string Email,
        [Display(Name = "Password:")] string Password,
        [Display(Name = "Password Confirm:")] string PasswordConfirm)
    {
        public static SignUpViewModel Empty => new (string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

        public static SignUpViewModel GetExampleModel => new("Ali", "KAYA", "AliKY", "ali@gmail.com", "Ali123*", "Ali123*");

    }

}
