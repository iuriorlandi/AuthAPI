using AuthAPI.Services.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Services.Models
{
    public class AlterPasswordModel : IAuthenticableUser
    {
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string NewPassword { get; set; }
    }
}
