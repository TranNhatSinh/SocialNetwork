﻿using System.ComponentModel.DataAnnotations;

namespace SocialNetworkAPI.DTOs
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
