using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.Core.Models.Domain
{
    public class User : IdentityUser
    {
        public ICollection<News> News { get; set; }

        public User()
        {
            News = new HashSet<News>();
        }
    }
}
