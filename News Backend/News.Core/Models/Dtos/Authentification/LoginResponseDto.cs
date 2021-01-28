using System;
using System.Collections.Generic;
using System.Text;
using News.Core.Models.Dtos.User;

namespace News.Core.Models.Authentification
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public UserReadDto User { get; set; }
    }
}
