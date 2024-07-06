using System.ComponentModel.DataAnnotations;

namespace e_learning.Application.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        public  string? EmailOrUsername { get; set; }

        [Required]
        public  string? Password { get; set; }
    }
}
