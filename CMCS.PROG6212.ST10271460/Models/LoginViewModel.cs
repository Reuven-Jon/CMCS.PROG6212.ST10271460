using System.ComponentModel.DataAnnotations;

namespace CMCS.PROG6212.ST10271460.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Username must be exactly 4 characters.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Password must be exactly 8 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; } = string.Empty;
    }
}




