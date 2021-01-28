using System;
using System.Collections.Generic;
using System.Text;

namespace News.Core.Models.Dtos.User
{
    public class UserReadDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
