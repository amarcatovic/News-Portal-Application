using System;
using System.Collections.Generic;
using System.Text;

namespace News.Core.Models.Domain
{
    public class UserEditedNews
    {
        public User User { get; set; }

        public string UserId { get; set; }

        public News News { get; set; }

        public int NewsId { get; set; }

        public DateTime DateEdited { get; set; }
    }
}
