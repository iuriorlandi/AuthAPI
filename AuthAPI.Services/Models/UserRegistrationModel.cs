using AuthAPI.Services.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Services.Models
{
    public class UserRegistrationModel
    {
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        [UniqueUsername]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }
    }
}
