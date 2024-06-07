﻿using System.ComponentModel.DataAnnotations;

namespace project.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Address Is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]

        [DataType(DataType.Password)]

        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
