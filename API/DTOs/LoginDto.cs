using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginDto
    {
        [Required]
        public required string EmailOrUsername { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
