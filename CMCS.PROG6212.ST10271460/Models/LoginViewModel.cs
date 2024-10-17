using System.ComponentModel.DataAnnotations;

namespace CMCS.PROG6212.ST10271460.Models
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(4, ErrorMessage = "Username must be exactly 4 characters.")]
        [MaxLength(4, ErrorMessage = "Username must be exactly 4 characters.")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(8, ErrorMessage = "Password must be exactly 8 characters.")]
        [MaxLength(8, ErrorMessage = "Password must be exactly 8 characters.")]
        [RegularExpression(@"^[^\s=+]*$", ErrorMessage = "Password cannot contain spaces or mathematical symbols.")]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;
    }
}


