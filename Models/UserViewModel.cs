using System.ComponentModel.DataAnnotations;
namespace Blogs.Models
{
    public class LoginModel
    {
        [Required]
        [UIHint("email")] // ensures the taghelper renders the appropriate form field
        public string Email { get; set; }

        [Required]
        [UIHint("password")] // ensures the taghelper renders the appropriate form field
        public string Password { get; set; }
    }
}
