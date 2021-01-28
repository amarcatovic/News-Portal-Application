using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace News.Core.Models.Authentification
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordRaw { get; set; }
    }
}
