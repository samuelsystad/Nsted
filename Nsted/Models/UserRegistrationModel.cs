using System.ComponentModel.DataAnnotations;

namespace Nsted.Models
{
    public class UserRegistrationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
