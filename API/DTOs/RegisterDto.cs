using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 6)]
        public required string Password { get; set; }

        [Required]
        public required string Name { get; set; }


        [Required]
        public required string Role { get; set; }

        // for instructors
        public string? Biography { get; set; }

    }
}
