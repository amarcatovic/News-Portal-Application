using System;
using System.Collections.Generic;
using System.Text;

namespace News.Core.Models.Authentification
{
    public class LoginDto
    {
        public string Email { get; set; }

        public string PasswordRaw { get; set; }
    }
}
