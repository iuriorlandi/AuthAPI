﻿using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Services.Models
{
    public class LoginModel : IAuthenticableUser
    {
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
